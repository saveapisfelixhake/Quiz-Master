"use client"

import { useEffect } from "react"
import { useRouter } from "next/navigation"
import { Button } from "@/components/ui/button"
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card"
import { Badge } from "@/components/ui/badge"
import { Users, Trophy, Play, User, LogOut, Beer, Star } from "lucide-react"
import { useQuizStore } from "@/lib/quiz-store"
import { getPlayerFromCookie, getTeamFromCookie } from "@/lib/cookie-utils";
import { Team } from "@/lib/types";

export default function DashboardPage() {
  const { currentPlayer, currentTeam, currentQuiz, currentBar, resetQuiz, removePlayerFromTeam, setCurrentTeam, setCurrentPlayer, setCurrentBar, logout } = useQuizStore()
  const router = useRouter()

  useEffect(() => {
    if (!currentTeam) {
      const teamFromCookie = getTeamFromCookie()
      if (teamFromCookie) {
        setCurrentTeam({
          ...teamFromCookie,
          players: [],
          createdAt: new Date(),
        });
      }
    }
    if (!currentPlayer) {
      const playerFromCookie = getPlayerFromCookie()
      if (playerFromCookie) {
        setCurrentPlayer({
          ...playerFromCookie,
          joinedAt: new Date(),
        })
      }
    }
    if (!currentBar) {
      setCurrentBar({
        name: "",
        id: "",
        code: "",
        teams: [],
        completeQuizes: 0,
        score: 0,
        createdAt: new Date(),
      });
    }

    if (!currentPlayer || !currentTeam || !currentBar) {
      router.push("/")
    }
  }, [currentPlayer, currentTeam, currentBar, router, setCurrentTeam, setCurrentPlayer])

  if (!currentPlayer || !currentTeam || !currentBar) {
    return null
  }

  const handleLogout = () => {
    removePlayerFromTeam(currentPlayer, currentTeam)
    resetQuiz()
    logout();

    router.push("/")
  }

  return (
    <div className="min-h-screen bg-background">
      <div className="container mx-auto px-4 py-8">
        {/* Header */}
        <div className="flex items-center justify-between mb-8">
          <div>
            <h1 className="text-3xl font-bold text-gray-900 dark:text-white">Quiz Dashboard</h1>
            <p className="text-gray-600 dark:text-gray-300">Willkommen zurück, {currentPlayer.name}!</p>
          </div>
          <Button onClick={handleLogout}>
            <LogOut className="h-4 w-4 mr-2" />
            Abmelden
          </Button>
        </div>

        <Button onClick={() => router.push("/quiz")} className="mb-5 w-full bg-secondary text-secondary-foreground">
          <Play className="h-4 w-4 mr-2" />
          Quiz spielen
        </Button>
        <div className="grid lg:grid-cols-3 gap-6">

          {/* Team Info */}
          <Card>
            <CardHeader>
              <CardTitle className="flex items-center gap-2">
                <Users className="h-5 w-5" />
                Team-Status
              </CardTitle>
            </CardHeader>
            <CardContent className="space-y-3">
              <div>
                <p className="text-sm text-gray-600 dark:text-gray-400">Team</p>
                <p className="font-medium">{currentTeam.name}</p>
              </div>
              <div>
                <p className="text-sm text-gray-600 dark:text-gray-400">Team-Code</p>
                <Badge variant="secondary">{currentTeam.code}</Badge>
              </div>
              <div>
                <p className="text-sm text-gray-600 dark:text-gray-400">Aktuelle Punkte</p>
                <p className="text-2xl font-bold text-primary">{currentTeam.score}</p>
              </div>
              <div>
                <p className="text-sm text-gray-600 dark:text-gray-400">Teammitglieder</p>
                <p className="font-medium">{currentTeam.players.length + 5} {currentTeam.players.length + 5 === 1 ? "Spieler*in" : "Spieler*innen"}</p>
              </div>
              <Button onClick={() => router.push("/team")}>
                Team verwalten
              </Button>
            </CardContent>
          </Card>

          {/* Bar Info */}
          <Card>
            <CardHeader>
              <CardTitle className="flex items-center gap-2">
                <Beer className="h-5 w-5" />
                Bar-Info
              </CardTitle>
            </CardHeader>
            <CardContent className="space-y-3">
              <div>
                <p className="text-sm text-gray-600 dark:text-gray-400">Name</p>
                <p className="font-medium">{currentBar.name}</p>
              </div>
              <div>
                <p className="text-sm text-gray-600 dark:text-gray-400">Teams</p>
                <p className="font-medium">{currentBar.teams.length + 1} {currentBar.teams.length + 1 === 1 ? "Team" : "Teams"}</p>
              </div>
              <div>
                <p className="text-sm text-gray-600 dark:text-gray-400">Quize abgeschlossen</p>
                <p className="font-medium">{currentBar.completeQuizes}</p>
              </div>
              <div>
                <p className="text-sm text-gray-600 dark:text-gray-400">Punkte</p>
                <p className="font-medium">{currentBar.score}</p>
              </div>
            </CardContent>
          </Card>

          {/* Bar Info */}
          <Card>
            <CardHeader>
              <CardTitle className="flex items-center gap-2">
                <Star className="h-5 w-5" />
                Prämien-Info
              </CardTitle>
            </CardHeader>
            <CardContent className="relative flex">
              <div className="space-y-3" >
                <div>
                  <p className="text-sm text-gray-600 dark:text-gray-400">250 Punkte</p>
                  <p className="font-medium">Ein Kaltgetränke</p>
                </div>
                <div>
                  <p className="text-sm text-gray-600 dark:text-gray-400">480 Punkte</p>
                  <p className="font-medium">Zwei Kaltgetränke</p>
                </div>
                <div>
                  <p className="text-sm text-gray-600 dark:text-gray-400">700 Punkte</p>
                  <p className="font-medium">Drei Kaltgetränke</p>
                </div>
                <div>
                  <p className="text-sm text-gray-600 dark:text-gray-400">900 Punkte</p>
                  <p className="font-medium">Vier Kaltgetränke</p>
                </div>
              </div>
              <div className="space-y-3 absolute bottom-0 right-5" >
                <img className="w-28 h-28" src=" /qr/price.png" />
              </div>
            </CardContent>
          </Card>

          {/* Quiz Status */}
          <Card>
            <CardHeader>
              <CardTitle className="flex items-center gap-2">
                <Play className="h-5 w-5" />
                Quiz-Status
              </CardTitle>
            </CardHeader>
            <CardContent className="space-y-3">
              {currentQuiz ? (
                <>
                  <div>
                    <p className="text-sm text-gray-600 dark:text-gray-400">Aktuelles Quiz</p>
                    <p className="font-medium">{currentQuiz.title}</p>
                  </div>
                  <div>
                    <p className="text-sm text-gray-600 dark:text-gray-400">Fragen</p>
                    <p className="font-medium">{currentQuiz.questions.length} Fragen</p>
                  </div>
                  <Badge variant={currentQuiz.isActive ? "default" : "secondary"}>
                    {currentQuiz.isActive ? "Aktiv" : "Beendet"}
                  </Badge>
                  {currentQuiz.isActive && (
                    <Button onClick={() => router.push("/quiz")} className="w-full">
                      Quiz starten
                    </Button>
                  )}
                </>
              ) : (
                <p className="text-gray-600 dark:text-gray-400">Kein aktives Quiz verfügbar</p>
              )}
            </CardContent>
          </Card>
        </div >

        {/* Quick Actions */}
        < div className="mt-8" >
          <h2 className="text-xl font-semibold mb-4 text-gray-900 dark:text-white">Schnellzugriff</h2>
          <div className="grid sm:grid-cols-2 lg:grid-cols-4 gap-4">
            <Button variant="outline" onClick={() => router.push("/leaderboard")} className="h-20 flex-col gap-2 bg-secondary text-secondary-foreground">
              <Trophy className="h-6 w-6" />
              Bestenliste
            </Button>
            <Button variant="outline" onClick={() => router.push("/team")} className="h-20 flex-col gap-2 bg-secondary text-secondary-foreground">
              <Users className="h-6 w-6" />
              Team
            </Button>
            <Button variant="outline" onClick={() => router.push("/profile")} className="h-20 flex-col gap-2 bg-secondary text-secondary-foreground">
              <User className="h-6 w-6" />
              Profil
            </Button>
          </div>
        </div >
      </div >
    </div >
  )
}
