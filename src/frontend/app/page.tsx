"use client"

import type React from "react"

import { useState } from "react"
import { Button } from "@/components/ui/button"
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "@/components/ui/card"
import { Input } from "@/components/ui/input"
import { Label } from "@/components/ui/label"
import { QrCode, Users, Trophy, Zap, ScanQrCode } from "lucide-react"
import { useQuizStore } from "@/lib/quiz-store"
import { useRouter } from "next/navigation"

export default function HomePage() {
  const [showRegistration, setShowRegistration] = useState(false)
  const router = useRouter()

  return (
    <div className="min-h-screen bg-background">

      <div className="container mx-auto px-4 py-8">
        {/* Header */}
        <div className="text-center mb-12">
          <div className="flex items-center justify-center gap-3 mb-4">
            <div className="p-3 bg-primary rounded-xl">
              <Zap className="h-8 w-8 text-white" />
            </div>
            <h1 className="text-4xl font-bold text-gray-900 dark:text-white">Rätselrausch</h1>
          </div>
          <p className="text-xl text-gray-600 dark:text-gray-300 max-w-2xl mx-auto text-balance">
            Treten Sie Ihrem Team bei und zeigen Sie Ihr Wissen in spannenden Quiz-Duellen
          </p>
        </div>

        {/* Registration Section */}
        <div className="max-w-md mx-auto mb-6">
          {!showRegistration ? (
            <Card>
              <CardHeader className="text-center">
                <CardTitle>Bereit zum Spielen?</CardTitle>
                <CardDescription>Melden Sie sich bei Ihrem Team an und starten Sie das Quiz</CardDescription>
              </CardHeader>
              <CardContent className="space-y-4">
                <Button onClick={() => setShowRegistration(true)} className="w-full" size="lg">
                  Jetzt anmelden
                </Button>
                <Button variant="outline" onClick={() => router.push("/leaderboard")} className="w-full">
                  Bestenliste ansehen
                </Button>
              </CardContent>
            </Card>
          ) : (
            <PlayerRegistrationForm />
          )}
        </div>

        {/* Features Grid */}
        <div className="grid md:grid-cols-3 gap-6">
          <Card className="text-center">
            <CardHeader>
              <QrCode className="h-12 w-12 text-secondary mx-auto mb-2" />
              <CardTitle>QR-Code Anmeldung</CardTitle>
            </CardHeader>
            <CardContent>
              <CardDescription>Scannen Sie den QR-Code oder geben Sie den Team-Code ein</CardDescription>
            </CardContent>
          </Card>

          <Card className="text-center">
            <CardHeader>
              <Users className="h-12 w-12 text-secondary mx-auto mb-2" />
              <CardTitle>Team-Spiel</CardTitle>
            </CardHeader>
            <CardContent>
              <CardDescription>Arbeiten Sie mit Ihrem Team zusammen und sammeln Sie Punkte</CardDescription>
            </CardContent>
          </Card>

          <Card className="text-center">
            <CardHeader>
              <Trophy className="h-12 w-12 text-secondary mx-auto mb-2" />
              <CardTitle>Bestenliste</CardTitle>
            </CardHeader>
            <CardContent>
              <CardDescription>Verfolgen Sie Ihre Position im Live-Ranking</CardDescription>
            </CardContent>
          </Card>
        </div>


      </div>
    </div>
  )
}

function PlayerRegistrationForm() {
  const [playerName, setPlayerName] = useState("")
  const [barCode, setbarCode] = useState("")
  const [teamCode, setTeamCode] = useState("")
  const [error, setError] = useState("")
  const [isLoading, setIsLoading] = useState(false)

  const { joinTeam } = useQuizStore()
  const router = useRouter()

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault()
    setError("")
    setIsLoading(true)

    if (!playerName.trim() || !teamCode.trim()) {
      setError("Bitte füllen Sie alle Felder aus")
      setIsLoading(false)
      return
    }

    const player = {
      id: Date.now().toString(),
      name: playerName.trim(),
      teamId: "",
      joinedAt: new Date(),
    }

    const success = joinTeam(player, teamCode.toUpperCase(), barCode)

    if (success) {
      router.push("/dashboard")
    } else {
      setError("Team-Code nicht gefunden. Bitte überprüfen Sie den Code.")
    }

    setIsLoading(false)
  }

  return (
    <Card>
      <CardHeader className="text-center">
        <CardTitle>Team beitreten</CardTitle>
        <CardDescription>Gib deinen Namen, euren Teamcode und euren Tischcode ein</CardDescription>
      </CardHeader>
      <CardContent>
        <form onSubmit={handleSubmit} className="space-y-4">
          <div className="space-y-2">
            <Label htmlFor="playerName">Ihr Name</Label>
            <Input
              id="playerName"
              type="text"
              placeholder="Max Mustermann"
              value={playerName}
              onChange={(e) => setPlayerName(e.target.value)}
              disabled={isLoading}
            />
          </div>

          <div className="flex items-center gap-2"><div className="space-y-2">
            <Label htmlFor="teamCode">Team-Code</Label>
            <Input
              id="teamCode"
              type="text"
              placeholder="MSHACK25"
              value={teamCode}
              onChange={(e) => setTeamCode(e.target.value.toUpperCase())}
              disabled={isLoading}
            />
          </div>

            <div className="space-y-2">
              <Label htmlFor="teamCode">Bar-Code</Label>
              <div className="flex items-center gap-2">
                <Input
                  id="barCode"
                  type="text"
                  placeholder="BLAU"
                  value={barCode}
                  onChange={(e) => setbarCode(e.target.value)}
                  disabled={isLoading}
                  className="flex 1"
                />
                <Button
                  onClick={(e) => e.preventDefault()} variant="outline" size="icon" className="bg-primary">
                  <ScanQrCode
                    style={{ width: "80%", height: "80%" }} className="text-primary-foreground" />
                </Button>
              </div>
            </div></div>



          {error && <div className="text-sm text-red-600 bg-red-50 dark:bg-red-900/20 p-3 rounded-md">{error}</div>}

          <Button type="submit" className="w-full" disabled={isLoading}>
            {isLoading ? "Anmeldung läuft..." : "Team beitreten"}
          </Button>
        </form>
      </CardContent>
    </Card >
  )
}
