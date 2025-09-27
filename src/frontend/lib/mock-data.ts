import type { Team, Player, Question, Quiz, LeaderboardEntry, Bar } from "./types"

export const mockBars: Bar[] = [
  {
    id: "1",
    name: "Blaues Haus",
    code: "BLAU",
    teams: [],
    completeQuizes: 2,
    score: 1680,
    createdAt: new Date("2025-01-15"),
    avatar: null,
  },
  {
    id: "4",
    name: "items",
    code: "ITEMS123",
    teams: [],
    completeQuizes: 0,
    score: 1535,
    createdAt: new Date("2025-01-15"),
    avatar: "/bars/items.jpg",
  },
  {
    id: "5",
    name: "viadee",
    code: "VIADEE",
    teams: [],
    completeQuizes: 0,
    score: 1000,
    createdAt: new Date("2025-01-15"),
    avatar: null,
  },
  {
    id: "6",
    name: "LVM",
    code: "LVM1896",
    teams: [],
    completeQuizes: 0,
    score: 800,
    createdAt: new Date("2025-01-15"),
    avatar: null,
  },
]
export const mockTeams: Team[] = [
  {
    id: "1",
    name: "Rätselrausch",
    code: "MSHACK25",
    players: [],
    score: 750,
    createdAt: new Date("2025-01-15"),
  },
  {
    id: "2",
    name: "AIchhörnchen",
    code: "BB2025",
    players: [],
    score: 720,
    createdAt: new Date("2025-01-15"),
  },
  {
    id: "3",
    name: "Münster Easy",
    code: "QW2025",
    players: [],
    score: 680,
    createdAt: new Date("2025-01-15"),
  },
  {
    id: "4",
    name: "items",
    code: "ITEMS123",
    players: [],
    score: 1535,
    createdAt: new Date("2025-01-15"),
  },
  {
    id: "5",
    name: "viadee",
    code: "VIADEE",
    players: [],
    score: 1000,
    createdAt: new Date("2025-01-15"),
  },
  {
    id: "6",
    name: "LVM",
    code: "LVM1896",
    players: [],
    score: 800,
    createdAt: new Date("2025-01-15"),
  },
]

export const mockPlayers: Player[] = [
  {
    id: "1",
    name: "Stefanie Mollemeier",
    teamId: "4",
    joinedAt: new Date("2025-01-15"),
  },
  {
    id: "2",
    name: "Rita Helter",
    teamId: "5",
    joinedAt: new Date("2025-01-15"),
  },
  {
    id: "3",
    name: "Marcus Loskant",
    teamId: "6",
    joinedAt: new Date("2025-01-15"),
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
  title: "Allgemeinwissen Quiz 2025",
  questions: mockQuestions,
  isActive: true,
  startTime: new Date(),
  endTime: new Date(Date.now() + 3600000), // 1 hour from now
}

export const mockLeaderboard: LeaderboardEntry[] = [
  {
    teamId: "1",
    teamName: "Rätselrausch",
    score: 750,
    rank: 1,
    players: ["Christian", "Felix", "Florian", "Jens", "Jule", "Lara", "Leon", "Roman", "Selina"],
  },
  {
    teamId: "2",
    teamName: "AIchhörnchen",
    score: 720,
    rank: 2,
    players: ["Felix", "Felix", "Friedjof", "Julia", "Moritz", "Paul", "Sophia"],
  },
  {
    teamId: "3",
    teamName: "Münster Easy",
    score: 680,
    rank: 3,
    players: ["Alicia Kristina", "Bandik", "Franziska", "Frederik", "Jana", "Johannes", "Joscha", "Lisa", "Renja", "Robert", "Sarah", "Tim"],
  },
]
