"use client"

import { useEffect, useState } from "react"
import { useRouter } from "next/navigation"
import { Button } from "@/components/ui/button"
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card"
import { Input } from "@/components/ui/input"
import { Label } from "@/components/ui/label"
import { Avatar, AvatarFallback } from "@/components/ui/avatar"
import { Badge } from "@/components/ui/badge"
import { Separator } from "@/components/ui/separator"
import { Dialog, DialogContent, DialogHeader, DialogTitle, DialogTrigger } from "@/components/ui/dialog"
import { ArrowLeft, User, Edit, Save, X, Trophy, Users, Calendar, Target } from "lucide-react"
import { useQuizStore } from "@/lib/quiz-store"

export default function ProfilePage() {
  const { currentPlayer, currentTeam, leaderboard, resetQuiz, removePlayerFromTeam } = useQuizStore()
  const router = useRouter()
  const [isEditing, setIsEditing] = useState(false)
  const [editedName, setEditedName] = useState("")

  useEffect(() => {
    if (!currentPlayer || !currentTeam) {
      router.push("/")
      return
    }
    setEditedName(currentPlayer.name)
  }, [currentPlayer, currentTeam, router])

  if (!currentPlayer || !currentTeam) {
    return null
  }

  const getPlayerInitials = (name: string) => {
    return name
      .split(" ")
      .map((n) => n[0])
      .join("")
      .toUpperCase()
  }

  const formatDate = (date: Date) => {
    return new Intl.DateTimeFormat("de-DE", {
      day: "2-digit",
      month: "2-digit",
      year: "numeric",
      hour: "2-digit",
      minute: "2-digit",
    }).format(date)
  }

  const teamRank = leaderboard.findIndex((entry) => entry.teamId === currentTeam.id) + 1
  //const teamEntry = leaderboard.find((entry) => entry.teamId === currentTeam.id)

  const handleSaveProfile = () => {
    // In a real app, this would update the player in the store/database
    console.log("Saving profile:", editedName)
    setIsEditing(false)
  }

  const handleDeleteAccount = () => {
    removePlayerFromTeam(currentPlayer, currentTeam)
    resetQuiz()
    router.push("/")
  }

  return (
    <div className="min-h-screen bg-gradient-to-br from-blue-50 to-indigo-100 dark:from-gray-900 dark:to-gray-800">
      <div className="container mx-auto px-4 py-8">
        {/* Header */}
        <div className="flex items-center gap-4 mb-8">
          <Button variant="outline" size="icon" onClick={() => router.push("/dashboard")}>
            <ArrowLeft className="h-4 w-4" />
          </Button>
          <div>
            <h1 className="text-3xl font-bold text-gray-900 dark:text-white">Spieler-Profil</h1>
            <p className="text-gray-600 dark:text-gray-300">Verwalten Sie Ihre Profil-Einstellungen</p>
          </div>
        </div>

        <div className="grid lg:grid-cols-3 gap-6">
          {/* Profile Card */}
          <Card className="lg:col-span-2">
            <CardHeader>
              <div className="flex items-center justify-between">
                <CardTitle className="flex items-center gap-2">
                  <User className="h-5 w-5" />
                  Profil-Informationen
                </CardTitle>
                <Button variant="outline" size="sm" onClick={() => setIsEditing(!isEditing)}>
                  {isEditing ? <X className="h-4 w-4" /> : <Edit className="h-4 w-4" />}
                  {isEditing ? "Abbrechen" : "Bearbeiten"}
                </Button>
              </div>
            </CardHeader>
            <CardContent className="space-y-6">
              {/* Avatar and Basic Info */}
              <div className="flex items-center gap-6">
                <Avatar className="h-24 w-24">
                  <AvatarFallback className="bg-blue-600 text-white text-2xl">
                    {getPlayerInitials(currentPlayer.name)}
                  </AvatarFallback>
                </Avatar>
                <div className="flex-1">
                  {isEditing ? (
                    <div className="space-y-2">
                      <Label htmlFor="playerName">Name</Label>
                      <div className="flex items-center gap-2">
                        <Input
                          id="playerName"
                          value={editedName}
                          onChange={(e) => setEditedName(e.target.value)}
                          className="max-w-xs"
                        />
                        <Button size="sm" onClick={handleSaveProfile}>
                          <Save className="h-4 w-4" />
                        </Button>
                      </div>
                    </div>
                  ) : (
                    <div>
                      <h2 className="text-2xl font-bold">{currentPlayer.name}</h2>
                      <p className="text-gray-600 dark:text-gray-400">Quiz-Spieler</p>
                    </div>
                  )}
                </div>
              </div>

              <Separator />

              {/* Profile Details */}
              <div className="grid sm:grid-cols-2 gap-6">
                <div>
                  <Label className="text-sm text-gray-600 dark:text-gray-400">Spieler-ID</Label>
                  <p className="font-mono text-sm bg-gray-100 dark:bg-gray-800 p-2 rounded">{currentPlayer.id}</p>
                </div>
                <div>
                  <Label className="text-sm text-gray-600 dark:text-gray-400">Beigetreten am</Label>
                  <p className="font-medium">{formatDate(currentPlayer.joinedAt)}</p>
                </div>
                <div>
                  <Label className="text-sm text-gray-600 dark:text-gray-400">Aktuelles Team</Label>
                  <div className="flex items-center gap-2">
                    <p className="font-medium">{currentTeam.name}</p>
                    <Badge variant="secondary">{currentTeam.code}</Badge>
                  </div>
                </div>
                <div>
                  <Label className="text-sm text-gray-600 dark:text-gray-400">Team-Position</Label>
                  <p className="font-medium">
                    {teamRank > 0 ? `#${teamRank} von ${leaderboard.length}` : "Nicht gerankt"}
                  </p>
                </div>
              </div>
            </CardContent>
          </Card>

          {/* Stats and Actions */}
          <div className="space-y-6">
            {/* Statistics */}
            <Card>
              <CardHeader>
                <CardTitle className="flex items-center gap-2">
                  <Trophy className="h-5 w-5" />
                  Statistiken
                </CardTitle>
              </CardHeader>
              <CardContent className="space-y-4">
                <div className="text-center">
                  <p className="text-3xl font-bold text-blue-600">{currentTeam.score}</p>
                  <p className="text-sm text-gray-600 dark:text-gray-400">Team-Punkte</p>
                </div>
                <div className="text-center">
                  <p className="text-2xl font-bold text-green-600">#{teamRank || "N/A"}</p>
                  <p className="text-sm text-gray-600 dark:text-gray-400">Team-Rang</p>
                </div>
                <div className="text-center">
                  <p className="text-2xl font-bold text-purple-600">{currentTeam.players.length}</p>
                  <p className="text-sm text-gray-600 dark:text-gray-400">Teammitglieder</p>
                </div>
              </CardContent>
            </Card>

            {/* Quick Actions */}
            <Card>
              <CardHeader>
                <CardTitle>Schnellaktionen</CardTitle>
              </CardHeader>
              <CardContent className="space-y-3">
                <Button onClick={() => router.push("/quiz")} className="w-full">
                  <Target className="h-4 w-4 mr-2" />
                  Quiz spielen
                </Button>
                <Button variant="outline" onClick={() => router.push("/team")} className="w-full">
                  <Users className="h-4 w-4 mr-2" />
                  Team verwalten
                </Button>
                <Button variant="outline" onClick={() => router.push("/leaderboard")} className="w-full">
                  <Trophy className="h-4 w-4 mr-2" />
                  Bestenliste
                </Button>
              </CardContent>
            </Card>

            {/* Account Settings */}
            <Card>
              <CardHeader>
                <CardTitle>Account-Einstellungen</CardTitle>
              </CardHeader>
              <CardContent className="space-y-3">
                <Dialog>
                  <DialogTrigger asChild>
                    <Button variant="destructive" className="w-full">
                      Account löschen
                    </Button>
                  </DialogTrigger>
                  <DialogContent>
                    <DialogHeader>
                      <DialogTitle>Account löschen</DialogTitle>
                    </DialogHeader>
                    <div className="space-y-4">
                      <p className="text-gray-600 dark:text-gray-400">
                        Sind Sie sicher, dass Sie Ihren Account löschen möchten? Diese Aktion kann nicht rückgängig
                        gemacht werden.
                      </p>
                      <div className="flex gap-2">
                        <Button variant="destructive" onClick={handleDeleteAccount} className="flex-1">
                          Ja, löschen
                        </Button>
                        <Button variant="outline" className="flex-1 bg-transparent">
                          Abbrechen
                        </Button>
                      </div>
                    </div>
                  </DialogContent>
                </Dialog>
              </CardContent>
            </Card>
          </div>
        </div>

        {/* Achievement Section */}
        <Card className="mt-8">
          <CardHeader>
            <CardTitle className="flex items-center gap-2">
              <Trophy className="h-5 w-5" />
              Erfolge & Aktivitäten
            </CardTitle>
          </CardHeader>
          <CardContent>
            <div className="grid sm:grid-cols-2 lg:grid-cols-4 gap-4">
              <div className="text-center p-4 bg-blue-50 dark:bg-blue-900/20 rounded-lg">
                <Calendar className="h-8 w-8 text-blue-600 mx-auto mb-2" />
                <p className="font-semibold">Erstes Quiz</p>
                <p className="text-sm text-gray-600 dark:text-gray-400">{formatDate(currentPlayer.joinedAt)}</p>
              </div>
              <div className="text-center p-4 bg-green-50 dark:bg-green-900/20 rounded-lg">
                <Users className="h-8 w-8 text-green-600 mx-auto mb-2" />
                <p className="font-semibold">Team beigetreten</p>
                <p className="text-sm text-gray-600 dark:text-gray-400">{currentTeam.name}</p>
              </div>
              <div className="text-center p-4 bg-purple-50 dark:bg-purple-900/20 rounded-lg">
                <Trophy className="h-8 w-8 text-purple-600 mx-auto mb-2" />
                <p className="font-semibold">Beste Position</p>
                <p className="text-sm text-gray-600 dark:text-gray-400">#{teamRank || "N/A"}</p>
              </div>
              <div className="text-center p-4 bg-yellow-50 dark:bg-yellow-900/20 rounded-lg">
                <Target className="h-8 w-8 text-yellow-600 mx-auto mb-2" />
                <p className="font-semibold">Höchste Punkte</p>
                <p className="text-sm text-gray-600 dark:text-gray-400">{currentTeam.score}</p>
              </div>
            </div>
          </CardContent>
        </Card>
      </div>
    </div>
  )
}
