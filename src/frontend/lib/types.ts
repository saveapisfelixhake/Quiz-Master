export interface Player {
  id: string
  name: string
  avatar?: string
  teamId: string
  joinedAt: Date
}

export interface Bar {
  id: string
  name: string
  code: string // QR code or manual entry code
  teams: Team[]
  completeQuizes: number
  score: number
  createdAt: Date
  avatar?: string
}

export interface Team {
  id: string
  name: string
  code: string // QR code or manual entry code
  players: Player[]
  score: number
  createdAt: Date
}

export interface Question {
  id: string
  text: string
  type: "single-choice" | "multiple-choice" | "free-text"
  options?: string[] // For choice questions
  correctAnswer?: string | string[] // For validation
  points: number
}

export interface Answer {
  id: string
  questionId: string
  teamId: string
  playerId: string
  answer: string | string[]
  submittedAt: Date
  isCorrect?: boolean
  points?: number
}

export interface Quiz {
  id: string
  title: string
  questions: Question[]
  isActive: boolean
  startTime?: Date
  endTime?: Date
}

export interface LeaderboardEntry {
  teamId: string
  teamName: string
  score: number
  rank: number
  players: string[]
}
