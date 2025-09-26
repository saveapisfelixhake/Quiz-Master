import { create } from "zustand"
import type { Player, Team, Quiz, Bar, Answer, LeaderboardEntry } from "./types"
import { mockTeams, mockPlayers, mockQuiz, mockLeaderboard, mockBars } from "./mock-data"

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

  // Actions
  setCurrentPlayer: (player: Player) => void
  setCurrentTeam: (team: Team) => void
  setCurrentBar: (bar: Bar) => void
  joinTeam: (player: Player, teamCode: string, barCode: string) => boolean
  submitAnswer: (answer: Omit<Answer, "id" | "submittedAt">) => void
  updateLeaderboard: () => void
  resetQuiz: () => void
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

  // Actions
  setCurrentPlayer: (player) => set({ currentPlayer: player }),

  setCurrentTeam: (team) => set({ currentTeam: team }),

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
}))
