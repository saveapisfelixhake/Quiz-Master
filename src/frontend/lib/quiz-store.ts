import { create } from "zustand"
import type { Player, Team, Quiz, Bar, Answer, LeaderboardEntry } from "./types"
import { mockTeams, mockPlayers, mockQuiz, mockLeaderboard, mockBars } from "./mock-data"
import {
  savePlayerToCookie,
  getPlayerFromCookie,
  saveTeamToCookie,
  getTeamFromCookie,
  clearPlayerCookies,
  type CookiePlayer,
  type CookieTeam,
} from "./cookie-utils"

interface QuizStore {
  // State
  currentPlayer: Player | null
  currentBar: Bar | null
  currentTeam: Team | null
  teams: Team[]
  bars: Bar[]
  players: Player[]
  currentQuiz: Quiz | null
  answers: Answer[]
  leaderboard: LeaderboardEntry[]
  isInitialized: boolean


  // Actions
  updatePlayerProfile: (updates: Partial<Player>) => void
  logout: () => void
  initializeFromCookies: () => void
  setCurrentPlayer: (player: Player) => void
  setCurrentTeam: (team: Team) => void
  setCurrentBar: (bar: Bar) => void
  joinTeam: (player: Player, teamCode: string, barCode: string) => boolean
  submitAnswer: (answer: Omit<Answer, "id" | "submittedAt">) => void
  updateLeaderboard: () => void
  resetQuiz: () => void
  removePlayerFromTeam: (currentPlayer: Player, currentTeam: Team) => void
}

function playerToCookie(player: Player): CookiePlayer {
  return {
    ...player,
    joinedAt: player.joinedAt.toISOString(),
  }
}

function cookieToPlayer(cookiePlayer: CookiePlayer): Player {
  return {
    ...cookiePlayer,
    joinedAt: new Date(cookiePlayer.joinedAt),
  }
}

function teamToCookie(team: Team): CookieTeam {
  return {
    id: team.id,
    name: team.name,
    code: team.code,
    score: team.score,
    createdAt: team.createdAt.toISOString(),
  }
}

function cookieToTeam(cookieTeam: CookieTeam, players: Player[] = []): Team {
  return {
    id: cookieTeam.id,
    name: cookieTeam.name,
    code: cookieTeam.code,
    score: cookieTeam.score,
    players: players,
    createdAt: new Date(cookieTeam.createdAt),
  }
}

export const useQuizStore = create<QuizStore>((set, get) => ({
  // Initial state
  currentPlayer: null,
  currentTeam: null,
  currentBar: null,
  teams: mockTeams,
  bars: mockBars,
  players: mockPlayers,
  currentQuiz: mockQuiz,
  answers: [],
  leaderboard: mockLeaderboard,
  isInitialized: false,

  updatePlayerProfile: (updates) => {
  set((state) => {
    if (!state.currentPlayer) return {}

    const updatedPlayer: Player = { ...state.currentPlayer, ...updates }

    // players-Liste aktualisieren
    const players = state.players.map(p => p.id === updatedPlayer.id ? updatedPlayer : p)

    // currentTeam.players aktualisieren (falls im Team)
    const currentTeam = state.currentTeam
      ? {
          ...state.currentTeam,
          players: state.currentTeam.players.map(p => p.id === updatedPlayer.id ? updatedPlayer : p)
        }
      : null

    // teams[] aktualisieren, damit der Player auch dort konsistent ist
    const teams = state.teams.map(t =>
      t.id === currentTeam?.id
        ? { ...t, players: t.players.map(p => p.id === updatedPlayer.id ? updatedPlayer : p) }
        : t
    )

    if (typeof window !== "undefined") {
      savePlayerToCookie(playerToCookie(updatedPlayer))
    }

    return {
      currentPlayer: updatedPlayer,
      players,
      currentTeam,
      teams,
    }
  })
},

  // Actions
  initializeFromCookies: () => {
    if (typeof window === "undefined") return

    const cookiePlayer = getPlayerFromCookie()
    const cookieTeam = getTeamFromCookie()

    if (cookiePlayer && cookieTeam) {
      const player = cookieToPlayer(cookiePlayer)
      const team = cookieToTeam(cookieTeam, [player])

      set({
        currentPlayer: player,
        currentTeam: team,
        isInitialized: true,
      })
    } else {
      set({ isInitialized: true })
    }
  },

  setCurrentPlayer: (player) => {
    set({ currentPlayer: player })
    if (typeof window !== "undefined") {
      savePlayerToCookie(playerToCookie(player))
    }
  },

 setCurrentTeam: (team) => {
    set({ currentTeam: team })
    if (typeof window !== "undefined") {
      saveTeamToCookie(teamToCookie(team))
    }
  },

  setCurrentBar: (bar) => set({ currentBar: bar }),

  joinTeam: (player, teamCode, barCode) => {
    const { teams, bars } = get()
    const team = teams.find((t) => t.code === teamCode)
    const bar = bars.find((b) => b.code === barCode)

    if (!team || !bar) return false

    const updatedPlayer = { ...player, teamId: team.id }
    const updatedTeam = {
      ...team,
      players: [...team.players, updatedPlayer],
    }
    const updatedBar = { ...bar, teams: [...bar.teams, updatedTeam] }

    set((state) => ({
      currentPlayer: updatedPlayer,
      currentTeam: updatedTeam,
      currentBar: updatedBar,
      bars: state.bars.map((b) => (b.id === bar.id ? updatedBar : b)),
      teams: state.teams.map((t) => (t.id === team.id ? updatedTeam : t)),
      players: [...state.players, updatedPlayer],
    }))

    return true
  },

  submitAnswer: (answerData) => {
    const newAnswer: Answer = {
      ...answerData,
      id: Date.now().toString(),
      submittedAt: new Date(),
    }

    set((state) => ({
      answers: [...state.answers, newAnswer],
    }))
  },
  logout: () => {

  },

  updateLeaderboard: () => {
    // This would calculate scores based on answers in a real implementation
    set({ leaderboard: mockLeaderboard })
  },

  resetQuiz: () =>
    set({
      currentPlayer: null,
      currentTeam: null,
      currentBar: null,
      answers: [],
    }),

  removePlayerFromTeam: (currentPlayer: Player, currentTeam: Team) => {
    const { teams } = get(); // aktuelles Array holen

    const updatedTeams = teams.map(team =>
        team.id === currentTeam.id
            ? {
              ...team,
              players: team.players.filter(player => player.id !== currentPlayer.id),
            }
            : team
    );

    set({ teams: updatedTeams }); // State / Store aktualisieren
  }
}))
