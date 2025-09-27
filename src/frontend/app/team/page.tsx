"use client"

import { useEffect, useState } from "react"
import { useRouter } from "next/navigation"
import { Button } from "@/components/ui/button"
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card"
import { Badge } from "@/components/ui/badge"
import { Avatar, AvatarFallback } from "@/components/ui/avatar"
import { Input } from "@/components/ui/input"
import { Label } from "@/components/ui/label"
import { Dialog, DialogContent, DialogHeader, DialogTitle, DialogTrigger } from "@/components/ui/dialog"
import { Users, Crown, Copy, Check, ArrowLeft, UserPlus, QrCode } from "lucide-react"
import { useQuizStore } from "@/lib/quiz-store"
import Image from "next/image";
import qrCode from "../../assets/QRCodeTeamidMSHACK25.png";

export default function TeamPage() {
  const { currentPlayer, currentTeam, teams } = useQuizStore()
  const router = useRouter()
  const [copied, setCopied] = useState(false)

  useEffect(() => {
    if (!currentPlayer || !currentTeam) {
      router.push("/")
    }
  }, [currentPlayer, currentTeam, router])

  if (!currentPlayer || !currentTeam) {
    return null
  }

  const copyTeamCode = async () => {
    try {
      await navigator.clipboard.writeText(currentTeam.code)
      setCopied(true)
      setTimeout(() => setCopied(false), 2000)
    } catch (err) {
      console.error("Failed to copy team code:", err)
    }
  }

  const getPlayerInitials = (name: string) => {
    return name
      .split(" ")
      .map((n) => n[0])
      .join("")
      .toUpperCase()
  }

  const formatJoinDate = (date: Date) => {
    return new Intl.DateTimeFormat("de-DE", {
      day: "2-digit",
      month: "2-digit",
      year: "numeric",
      hour: "2-digit",
      minute: "2-digit",
    }).format(date)
  }

  return (
    <div className="min-h-screen bg-background">
      <div className="container mx-auto px-4 py-8">
        {/* Header */}
        <div className="flex items-center gap-4 mb-8">
          <Button variant="outline" size="icon" onClick={() => router.push("/dashboard")}>
            <ArrowLeft className="h-4 w-4" />
          </Button>
          <div>
            <h1 className="text-3xl font-bold text-gray-900 dark:text-white">Team Management</h1>
            <p className="text-gray-600 dark:text-gray-300">Verwalten Sie Ihr Team: {currentTeam.name}</p>
          </div>
        </div>

        <div className="grid lg:grid-cols-3 gap-6">
          {/* Team Overview */}
          <Card className="lg:col-span-2">
            <CardHeader>
              <div className="flex items-center justify-between">
                <CardTitle className="flex items-center gap-2">
                  <Users className="h-5 w-5" />
                  Team-Übersicht
                </CardTitle>
                <Badge variant="secondary" className="bg-secondary text-secondary-foreground text-lg px-3 py-1">
                  {currentTeam.score} Punkte
                </Badge>
              </div>
            </CardHeader>
            <CardContent className="space-y-6">
              {/* Team Info */}
              <div className="grid sm:grid-cols-2 gap-4">
                <div>
                  <p className="text-sm text-gray-600 dark:text-gray-400 mb-1">Team-Name</p>
                  <p className="text-xl font-semibold">{currentTeam.name}</p>
                </div>
                <div>
                  <p className="text-sm text-gray-600 dark:text-gray-400 mb-1">Erstellt am</p>
                  <p className="font-medium">{formatJoinDate(currentTeam.createdAt)}</p>
                </div>
              </div>

              {/* Team Code */}
              <div>
                <p className="text-sm text-gray-600 dark:text-gray-400 mb-2">Team-Code zum Beitreten</p>
                <div className="flex items-center gap-2">
                  <Badge variant="outline" className="text-lg px-4 py-2 font-mono">
                    {currentTeam.code}
                  </Badge>
                  <Button variant="outline" size="sm" onClick={copyTeamCode}>
                    {copied ? <Check className="h-4 w-4" /> : <Copy className="h-4 w-4" />}
                  </Button>
                </div>
                <p className="text-xs text-gray-500 mt-1">Teilen Sie diesen Code mit neuen Teammitgliedern</p>
              </div>

              {/* Team Members */}
              <div>
                <div className="flex items-center justify-between mb-4">
                  <h3 className="text-lg font-semibold">Teammitglieder ({currentTeam.players.length})</h3>
                  <Dialog>
                    <DialogTrigger asChild>
                      <Button variant="outline" className="bg-primary text-primary-foreground" size="sm">
                        <UserPlus className="h-4 w-4 mr-2" />
                        Einladen
                      </Button>
                    </DialogTrigger>
                    <DialogContent>
                      <DialogHeader>
                        <DialogTitle>Teammitglieder einladen</DialogTitle>
                      </DialogHeader>
                      <InviteDialog teamCode={currentTeam.code} />
                    </DialogContent>
                  </Dialog>
                </div>

                <div className="space-y-3">
                  {currentTeam.players.length > 0 ? (
                    currentTeam.players.map((player) => (
                      <div
                        key={player.id}
                        className="flex items-center gap-3 p-3 bg-gray-50 dark:bg-gray-800 rounded-lg"
                      >
                        <Avatar>
                          <AvatarFallback className="bg-blue-600 text-white">
                            {getPlayerInitials(player.name)}
                          </AvatarFallback>
                        </Avatar>
                        <div className="flex-1">
                          <div className="flex items-center gap-2">
                            <p className="font-medium">{player.name}</p>
                            {player.id === currentPlayer.id && (
                              <Badge variant="secondary" className="text-xs">
                                Sie
                              </Badge>
                            )}
                          </div>
                          <p className="text-sm text-gray-600 dark:text-gray-400">
                            Beigetreten: {formatJoinDate(player.joinedAt)}
                          </p>
                        </div>
                        {player.id === currentTeam.players[0]?.id && (
                          <Crown className="h-5 w-5 text-secondary" />
                        )}
                      </div>
                    ))
                  ) : (
                    <div className="text-center py-8 text-gray-500">
                      <Users className="h-12 w-12 mx-auto mb-2 opacity-50" />
                      <p>Noch keine Teammitglieder</p>
                      <p className="text-sm">Laden Sie andere ein, um zu beginnen!</p>
                    </div>
                  )}
                </div>
              </div>
            </CardContent>
          </Card>

          {/* Team Stats */}
          <div className="space-y-6">
            <Card>
              <CardHeader>
                <CardTitle>Team-Statistiken</CardTitle>
              </CardHeader>
              <CardContent className="space-y-4">
                <div className="text-center">
                  <p className="text-3xl font-bold text-primary">{currentTeam.score}</p>
                  <p className="text-sm text-gray-600 dark:text-gray-400">Gesamtpunkte</p>
                </div>
                <div className="text-center">
                  <p className="text-2xl font-bold text-primary">{currentTeam.players.length}</p>
                  <p className="text-sm text-gray-600 dark:text-gray-400">Aktive Spieler</p>
                </div>
                <div className="text-center">
                  <p className="text-2xl font-bold text-primary">
                    {teams.findIndex((t) => t.id === currentTeam.id) + 1}
                  </p>
                  <p className="text-sm text-gray-600 dark:text-gray-400">Aktuelle Position</p>
                </div>
              </CardContent>
            </Card>

            <Card>
              <CardHeader>
                <CardTitle>Schnellaktionen</CardTitle>
              </CardHeader>
              <CardContent className="space-y-3">
                <Button onClick={() => router.push("/quiz")} className="w-full">
                  <Crown className="h-4 w-4 mr-2" />
                  Quiz starten
                </Button>
                <Button variant="outline" onClick={() => router.push("/leaderboard")} className="w-full">
                  Bestenliste ansehen
                </Button>
                <Button variant="outline" onClick={() => router.push("/profile")} className="w-full">
                  Profil bearbeiten
                </Button>
              </CardContent>
            </Card>
          </div>
        </div>
      </div>
    </div>
  )
}

function InviteDialog({ teamCode }: { teamCode: string }) {
  const [copied, setCopied] = useState(false)

  const copyInviteText = async () => {
    const inviteText = `Tritt meinem Quiz-Team bei! 🎯\n\nTeam-Code: ${teamCode}\n\nGehe zu Quiz Arena und gib den Code ein, um beizutreten!`

    try {
      await navigator.clipboard.writeText(inviteText)
      setCopied(true)
      setTimeout(() => setCopied(false), 2000)
    } catch (err) {
      console.error("Failed to copy invite text:", err)
    }
  }

  return (
    <div className="space-y-4">

      <div className="space-y-2">
        <p className="text-sm font-medium">So können andere beitreten:</p>
        <ol className="text-sm text-gray-600 dark:text-gray-400 space-y-1 list-decimal list-inside">
          <li>Quiz Arena öffnen</li>
          {/* <li>Auf "Jetzt anmelden" klicken</li> */}
          <li>Namen eingeben</li>
          <li>
            QR Code scannen oder Team-Code eingeben: <code className="bg-gray-100 dark:bg-gray-800 px-1 rounded">{teamCode}</code>
          </li>
        </ol>
      </div>

      <div>
        <Label>Team-Code</Label>
        <div className="flex items-center gap-2 mt-1">
          <Input value={teamCode} readOnly className="font-mono" />
          <Button variant="outline" size="sm" onClick={copyInviteText}>
            {copied ? <Check className="h-4 w-4" /> : <Copy className="h-4 w-4" />}
          </Button>
        </div>
      </div>

      <div className="pt-4 border-t">
        <div className="flex justify-center items-center gap-2 text-sm text-gray-600 dark:text-gray-400">
          <span><Image src={qrCode} alt="QR Code" className="h-24 w-24" /></span>
        </div>
      </div>
    </div>
  )
}
