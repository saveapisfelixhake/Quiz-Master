import type { Team, Player, Question, Quiz, LeaderboardEntry } from "./types"

export const mockTeams: Team[] = [
  {
    id: "1",
    name: "Die Quizmaster",
    code: "QM2024",
    players: [],
    score: 850,
    createdAt: new Date("2024-01-15"),
  },
  {
    id: "2",
    name: "Brain Busters",
    code: "BB2024",
    players: [],
    score: 720,
    createdAt: new Date("2024-01-15"),
  },
  {
    id: "3",
    name: "Quiz Warriors",
    code: "QW2024",
    players: [],
    score: 680,
    createdAt: new Date("2024-01-15"),
  },
]

export const mockPlayers: Player[] = [
  {
    id: "1",
    name: "Max Mustermann",
    teamId: "1",
    joinedAt: new Date("2024-01-15"),
  },
  {
    id: "2",
    name: "Anna Schmidt",
    teamId: "1",
    joinedAt: new Date("2024-01-15"),
  },
  {
    id: "3",
    name: "Tom Weber",
    teamId: "2",
    joinedAt: new Date("2024-01-15"),
  },
]

export const mockQuestions: Question[] = [
  {
    id: "1",
    text: "Welche ist die Hauptstadt von Deutschland?",
    type: "single-choice",
    options: ["Berlin", "München", "Hamburg", "Köln"],
    correctAnswer: "Berlin",
    points: 10,
  },
  {
    id: "2",
    text: "Welche Programmiersprachen werden für Webentwicklung verwendet?",
    type: "multiple-choice",
    options: ["JavaScript", "Python", "HTML", "CSS", "Java"],
    correctAnswer: ["JavaScript", "HTML", "CSS"],
    points: 15,
  },
  {
    id: "3",
    text: "Beschreiben Sie in einem Satz, was Künstliche Intelligenz ist.",
    type: "free-text",
    points: 20,
  },
]

export const mockQuiz: Quiz = {
  id: "1",
  title: "Allgemeinwissen Quiz 2024",
  questions: mockQuestions,
  isActive: true,
  startTime: new Date(),
  endTime: new Date(Date.now() + 3600000), // 1 hour from now
}

export const mockLeaderboard: LeaderboardEntry[] = [
  {
    teamId: "1",
    teamName: "Die Quizmaster",
    score: 850,
    rank: 1,
    players: ["Max Mustermann", "Anna Schmidt"],
  },
  {
    teamId: "2",
    teamName: "Brain Busters",
    score: 720,
    rank: 2,
    players: ["Tom Weber"],
  },
  {
    teamId: "3",
    teamName: "Quiz Warriors",
    score: 680,
    rank: 3,
    players: [],
  },
]
