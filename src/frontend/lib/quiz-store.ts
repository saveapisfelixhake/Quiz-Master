import { create } from "zustand"
import type { Player, Team, Quiz, Answer, LeaderboardEntry } from "./types"
import { mockTeams, mockPlayers, mockQuiz, mockLeaderboard } from "./mock-data"

interface QuizStore {
  // State
  currentPlayer: Player | null
  currentTeam: Team | null
  teams: Team[]
  players: Player[]
  currentQuiz: Quiz | null
  answers: Answer[]
  leaderboard: LeaderboardEntry[]

  // Actions
  setCurrentPlayer: (player: Player) => void
  setCurrentTeam: (team: Team) => void
  joinTeam: (player: Player, teamCode: string) => boolean
  submitAnswer: (answer: Omit<Answer, "id" | "submittedAt">) => void
  updateLeaderboard: () => void
  resetQuiz: () => void
}

export const useQuizStore = create<QuizStore>((set, get) => ({
  // Initial state
  currentPlayer: null,
  currentTeam: null,
  teams: mockTeams,
  players: mockPlayers,
  currentQuiz: mockQuiz,
  answers: [],
  leaderboard: mockLeaderboard,

  // Actions
  setCurrentPlayer: (player) => set({ currentPlayer: player }),

  setCurrentTeam: (team) => set({ currentTeam: team }),

  joinTeam: (player, teamCode) => {
    const { teams } = get()
    const team = teams.find((t) => t.code === teamCode)

    if (!team) return false

    const updatedPlayer = { ...player, teamId: team.id }
    const updatedTeam = {
      ...team,
      players: [...team.players, updatedPlayer],
    }

    set((state) => ({
      currentPlayer: updatedPlayer,
      currentTeam: updatedTeam,
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
      answers: [],
    }),
}))
