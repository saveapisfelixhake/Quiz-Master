"use client"

import { useState } from "react"
import { useRouter } from "next/navigation"
import { Button } from "@/components/ui/button"
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card"
import { Badge } from "@/components/ui/badge"
import { Avatar, AvatarFallback } from "@/components/ui/avatar"
import { Tabs, TabsContent, TabsList, TabsTrigger } from "@/components/ui/tabs"
import { Trophy, Medal, Award, Users, TrendingUp, ArrowLeft, Crown, Star } from "lucide-react"
import { useQuizStore } from "@/lib/quiz-store"
import { AvatarImage } from "@radix-ui/react-avatar"

export default function LeaderboardPage() {
  const { leaderboard, teams, bars, currentTeam } = useQuizStore()
  const router = useRouter()
  const [activeTab, setActiveTab] = useState("teams")

  const getPlayerInitials = (name: string) => {
    return name
      .split(" ")
      .map((n) => n[0])
      .join("")
      .toUpperCase()
  }

  const getRankIcon = (rank: number) => {
    switch (rank) {
      case 1:
        return <Trophy className="h-5 w-5 text-yellow-500" />
      case 2:
        return <Medal className="h-5 w-5 text-gray-400" />
      case 3:
        return <Award className="h-5 w-5 text-amber-600" />
      default:
        return <span className="text-lg font-bold text-gray-500">#{rank}</span>
    }
  }

  const getRankBadgeColor = (rank: number) => {
    switch (rank) {
      case 1:
        return "bg-gradient-to-r from-yellow-400 to-yellow-600 text-white"
      case 2:
        return "bg-gradient-to-r from-gray-300 to-gray-500 text-white"
      case 3:
        return "bg-gradient-to-r from-amber-400 to-amber-600 text-white"
      default:
        return "bg-gray-100 dark:bg-gray-800 text-gray-700 dark:text-gray-300"
    }
  }

  const topBars = bars
    .map((bar) => {
      return {
        ...bar,
      }
    })
    .sort((a, b) => b.score - a.score)
    .slice(0, 10)

  return (
    <div className="min-h-screen bg-bg">
      <div className="container mx-auto px-4 py-8">
        {/* Header */}
        <div className="flex items-center gap-4 mb-8">
          <Button variant="outline" size="icon" onClick={() => router.push("/dashboard")}>
            <ArrowLeft className="h-4 w-4" />
          </Button>
          <div>
            <h1 className="text-3xl font-bold text-gray-900 dark:text-white">Bestenliste</h1>
            <p className="text-gray-600 dark:text-gray-300">Aktuelle Rangliste aller Teams und Bars</p>
          </div>
        </div>

        {/* Current Team Highlight */}
        {currentTeam && (
          <Card className="mb-8 border-2 border-blue-200 dark:border-blue-800 bg-blue-50 dark:bg-blue-900/20">
            <CardHeader>
              <CardTitle className="flex items-center gap-2">
                <Star className="h-5 w-5 text-blue-600" />
                Ihr Team
              </CardTitle>
            </CardHeader>
            <CardContent>
              <div className="flex items-center justify-between">
                <div className="flex items-center gap-4">
                  <div className="flex items-center gap-2">
                    {getRankIcon(leaderboard.findIndex((entry) => entry.teamId === currentTeam.id) + 1)}
                    <div>
                      <p className="font-semibold text-lg">{currentTeam.name}</p>
                      <p className="text-sm text-gray-600 dark:text-gray-400">{currentTeam.players.length} Spieler</p>
                    </div>
                  </div>
                </div>
                <div className="text-right">
                  <p className="text-2xl font-bold text-blue-600">{currentTeam.score}</p>
                  <p className="text-sm text-gray-600 dark:text-gray-400">Punkte</p>
                </div>
              </div>
            </CardContent>
          </Card>
        )}

        {/* Tabs */}
        <Tabs value={activeTab} onValueChange={setActiveTab}>
          <TabsList className="grid w-full grid-cols-2 mb-6">
            <TabsTrigger value="teams" className="shadow-md flex items-center gap-2 data-[state=active]:bg-secondary data-[state=active]:text-secondary-foreground">
              <Users className="h-4 w-4" />
              Teams
            </TabsTrigger>
            <TabsTrigger value="bars" className="shadow-md flex items-center gap-2 data-[state=active]:bg-secondary data-[state=active]:text-secondary-foreground">
              <TrendingUp className="h-4 w-4" />
              Bars
            </TabsTrigger>
          </TabsList>

          {/* Teams Leaderboard */}
          <TabsContent value="teams">
            <div className="space-y-4">
              {leaderboard.map((entry, index) => (
                <Card
                  key={entry.teamId}
                  className={`transition-all hover:shadow-md ${entry.teamId === currentTeam?.id ? "ring-2 ring-blue-500" : ""
                    }`}
                >
                  <CardContent className="p-6">
                    <div className="flex items-center justify-between">
                      <div className="flex items-center gap-2">
                        <div
                          className={`w-12 h-12 rounded-full flex items-center flex-shrink-0 justify-center ${getRankBadgeColor(
                            entry.rank,
                          )}`}
                        >
                          {entry.rank <= 3 ? getRankIcon(entry.rank) : <span className="font-bold">#{entry.rank}</span>}
                        </div>
                        <div>
                          <div className="flex items-center gap-2">
                            <h3 className="text-xl font-semibold">{entry.teamName}</h3>
                            {entry.rank === 1 && <Crown className="h-5 w-5 text-secondary" />}
                            {entry.teamId === currentTeam?.id && (
                              <Badge variant="secondary" className="text-xs">
                                Ihr Team
                              </Badge>
                            )}
                          </div>
                          <p className="text-gray-600 dark:text-gray-400">
                            {entry.players.length > 0 ? entry.players.join(", ") : "Keine aktiven Spieler"}
                          </p>
                        </div>
                      </div>
                      <div className="text-right">
                        <p className="text-3xl font-bold text-primary">{entry.score}</p>
                        <p className="text-sm text-gray-600 dark:text-gray-400">Punkte</p>
                      </div>
                    </div>
                  </CardContent>
                </Card>
              ))}
            </div>
          </TabsContent>

          {/* Bars Leaderboard */}
          <TabsContent value="bars">
            <div className="space-y-4">
              {topBars.map((bar, index) => (
                <Card key={bar.id} className="transition-all hover:shadow-md" >
                  <CardContent className="p-6">
                    <div className="flex items-center justify-between">
                      <div className="flex items-center gap-2">
                        <div
                          className={`w-12 h-12 rounded-full flex flex-shrink-0 items-center justify-center ${getRankBadgeColor(
                            index + 1,
                          )}`}
                        >
                          {index + 1 <= 3 ? getRankIcon(index + 1) : <span className="font-bold">#{index + 1}</span>}
                        </div>
                        <Avatar className="h-12 w-12">
                          <AvatarImage src={bar.avatar} />
                          <AvatarFallback className="bg-blue-600 text-white">
                            {getPlayerInitials(bar.name)}
                          </AvatarFallback>
                        </Avatar>
                        <div>
                          <h3 className="text-xl font-semibold">{bar.name}</h3>
                        </div>
                      </div>
                      <div className="text-right">
                        <p className="text-3xl font-bold text-primary">{bar.score}</p>
                        <p className="text-sm text-gray-600 dark:text-gray-400">Punkte</p>
                      </div>
                    </div>
                  </CardContent>
                </Card>
              ))}
            </div>
          </TabsContent>
        </Tabs>

        {/* Stats Summary */}
        <div className="mt-12 grid sm:grid-cols-3 gap-6">
          <Card>
            <CardHeader className="text-center">
              <Trophy className="h-8 w-8 text-secondary mx-auto mb-2" />
              <CardTitle>Führendes Team</CardTitle>
            </CardHeader>
            <CardContent className="text-center">
              <p className="text-xl font-bold">{leaderboard[0]?.teamName || "Noch kein Sieger"}</p>
              <p className="text-gray-600 dark:text-gray-400">{leaderboard[0]?.score || 0} Punkte</p>
            </CardContent>
          </Card>

          <Card>
            <CardHeader className="text-center">
              <Users className="h-8 w-8 text-secondary mx-auto mb-2" />
              <CardTitle>Aktive Teams</CardTitle>
            </CardHeader>
            <CardContent className="text-center">
              <p className="text-xl font-bold">{teams.length}</p>
              <p className="text-gray-600 dark:text-gray-400">Teams im Wettbewerb</p>
            </CardContent>
          </Card>

          <Card>
            <CardHeader className="text-center">
              <TrendingUp className="h-8 w-8 text-secondary mx-auto mb-2" />
              <CardTitle>Gesamtspieler</CardTitle>
            </CardHeader>
            <CardContent className="text-center">
              <p className="text-xl font-bold">{bars.length}</p>
              <p className="text-gray-600 dark:text-gray-400">Registrierte Spieler</p>
            </CardContent>
          </Card>
        </div>

        {/* Action Buttons */}
        <div className="mt-8 flex justify-center gap-4">
          <Button onClick={() => router.push("/quiz")} size="lg">
            Quiz spielen
          </Button>
          <Button variant="outline" onClick={() => router.push("/team")} size="lg">
            Team verwalten
          </Button>
        </div>
      </div>
    </div >
  )
}
