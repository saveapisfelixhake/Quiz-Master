"use client"

import { useEffect, useState } from "react"
import { useRouter } from "next/navigation"
import { Button } from "@/components/ui/button"
import { Card, CardContent, CardHeader, CardTitle } from "@/components/ui/card"
import { Badge } from "@/components/ui/badge"
import { Progress } from "@/components/ui/progress"
import { RadioGroup, RadioGroupItem } from "@/components/ui/radio-group"
import { Checkbox } from "@/components/ui/checkbox"
import { Textarea } from "@/components/ui/textarea"
import { Label } from "@/components/ui/label"
import { ArrowLeft, Clock, Users, CheckCircle, AlertCircle, Trophy } from "lucide-react"
import { useQuizStore } from "@/lib/quiz-store"
import type { Question } from "@/lib/types"

export default function QuizPage() {
  const { currentPlayer, currentTeam, currentQuiz, submitAnswer } = useQuizStore()
  const router = useRouter()
  const [currentQuestionIndex, setCurrentQuestionIndex] = useState(0)
  const [answers, setAnswers] = useState<Record<string, string | string[]>>({})
  const [timeLeft, setTimeLeft] = useState(300) // 5 minutes per question
  const [isSubmitting, setIsSubmitting] = useState(false)
  const [quizCompleted, setQuizCompleted] = useState(false)

  useEffect(() => {
    if (!currentPlayer || !currentTeam || !currentQuiz) {
      router.push("/")
      return
    }

    // Timer countdown
    const timer = setInterval(() => {
      setTimeLeft((prev) => {
        if (prev <= 1) {
          handleNextQuestion()
          return 300
        }
        return prev - 1
      })
    }, 1000)

    return () => clearInterval(timer)
  }, [currentPlayer, currentTeam, currentQuiz, router])

  if (!currentPlayer || !currentTeam || !currentQuiz) {
    return null
  }

  const currentQuestion = currentQuiz.questions[currentQuestionIndex]
  const progress = ((currentQuestionIndex + 1) / currentQuiz.questions.length) * 100
  const isLastQuestion = currentQuestionIndex === currentQuiz.questions.length - 1

  const formatTime = (seconds: number) => {
    const mins = Math.floor(seconds / 60)
    const secs = seconds % 60
    return `${mins}:${secs.toString().padStart(2, "0")}`
  }

  const handleAnswerChange = (questionId: string, answer: string | string[]) => {
    setAnswers((prev) => ({
      ...prev,
      [questionId]: answer,
    }))
  }

  const handleNextQuestion = async () => {
    if (isSubmitting) return

    setIsSubmitting(true)

    // Submit current answer
    const currentAnswer = answers[currentQuestion.id]
    if (currentAnswer) {
      submitAnswer({
        questionId: currentQuestion.id,
        teamId: currentTeam.id,
        playerId: currentPlayer.id,
        answer: currentAnswer,
      })
    }

    // Move to next question or complete quiz
    if (isLastQuestion) {
      setQuizCompleted(true)
    } else {
      setCurrentQuestionIndex((prev) => prev + 1)
      setTimeLeft(300) // Reset timer for next question
    }

    setIsSubmitting(false)
  }

  const handlePreviousQuestion = () => {
    if (currentQuestionIndex > 0) {
      setCurrentQuestionIndex((prev) => prev - 1)
      setTimeLeft(300) // Reset timer
    }
  }

  if (quizCompleted) {
    return <QuizCompletedScreen />
  }

  return (
    <div className="min-h-screen bg-background">
      <div className="container mx-auto px-4 py-8">
        {/* Header */}
        <div className="flex items-center justify-between mb-6">
          <div className="flex items-center gap-4">
            <Button variant="outline" size="icon" onClick={() => router.push("/dashboard")}>
              <ArrowLeft className="h-4 w-4" />
            </Button>
            <div>
              <h1 className="text-2xl font-bold text-gray-900 dark:text-white">{currentQuiz.title}</h1>
              <p className="text-gray-600 dark:text-gray-300">Team: {currentTeam.name}</p>
            </div>
          </div>
          <div className="flex items-center gap-4">
            {/*<Badge variant="outline" className="flex items-center gap-2">
              <Users className="h-4 w-4" />
              {currentTeam.players.length} Spieler
            </Badge>*/}
            <Badge variant={timeLeft < 60 ? "destructive" : "secondary"} className="flex items-center gap-2">
              <Clock className="h-4 w-4" />
              {formatTime(timeLeft)}
            </Badge>
          </div>
        </div>

        {/* Progress */}
        <div className="mb-8">
          <div className="flex items-center justify-between mb-2">
            <p className="text-sm text-gray-600 dark:text-gray-400">
              Frage {currentQuestionIndex + 1} von {currentQuiz.questions.length}
            </p>
            <p className="text-sm font-medium">{Math.round(progress)}% abgeschlossen</p>
          </div>
          <Progress value={progress} className="h-2 [&>div]:bg-primary bg-secondary" />
        </div>

        {/* Question Card */}
        <Card className="mb-6">
          <CardHeader>
            <div className="flex items-center justify-between">
              <CardTitle className="text-xl text-balance">{currentQuestion.text}</CardTitle>
              <Badge variant="outline">{currentQuestion.points} Punkte</Badge>
            </div>
          </CardHeader>
          <CardContent>
            <QuestionInput
              question={currentQuestion}
              value={answers[currentQuestion.id]}
              onChange={(answer) => handleAnswerChange(currentQuestion.id, answer)}
            />
          </CardContent>
        </Card>

        {/* Navigation */}
        <div className="flex items-center justify-between">
          <Button variant="outline" onClick={handlePreviousQuestion} disabled={currentQuestionIndex === 0}>
            Vorherige Frage
          </Button>
          <Button
            onClick={handleNextQuestion}
            disabled={isSubmitting || !answers[currentQuestion.id]}
            className="min-w-32 bg-primary"
          >
            {isSubmitting ? "Wird übertragen..." : isLastQuestion ? "Quiz beenden" : "Nächste Frage"}
          </Button>
        </div>
      </div>
    </div >
  )
}

function QuestionInput({
  question,
  value,
  onChange,
}: {
  question: Question
  value: string | string[]
  onChange: (answer: string | string[]) => void
}) {
  if (question.type === "single-choice" && question.options) {
    return (
      <RadioGroup value={value as string} onValueChange={onChange}>
        <div className="space-y-3">
          {question.options.map((option, index) => (
            <div key={index} className="flex items-center space-x-3">
              <RadioGroupItem value={option} id={`option-${index}`} />
              <Label htmlFor={`option-${index}`} className="flex-1 cursor-pointer">
                {option}
              </Label>
            </div>
          ))}
        </div>
      </RadioGroup>
    )
  }

  if (question.type === "multiple-choice" && question.options) {
    const selectedValues = (value as string[]) || []

    return (
      <div className="space-y-3">
        {question.options.map((option, index) => (
          <div key={index} className="flex items-center space-x-3">
            <Checkbox
              id={`option-${index}`}
              checked={selectedValues.includes(option)}
              onCheckedChange={(checked) => {
                if (checked) {
                  onChange([...selectedValues, option])
                } else {
                  onChange(selectedValues.filter((v) => v !== option))
                }
              }}
            />
            <Label htmlFor={`option-${index}`} className="flex-1 cursor-pointer">
              {option}
            </Label>
          </div>
        ))}
      </div>
    )
  }

  if (question.type === "free-text") {
    return (
      <Textarea
        placeholder="Geben Sie Ihre Antwort hier ein..."
        value={(value as string) || ""}
        onChange={(e) => onChange(e.target.value)}
        className="min-h-32"
      />
    )
  }

  return null
}

function QuizCompletedScreen() {
  const { currentTeam, resetQuiz } = useQuizStore()
  const router = useRouter()

  const handleViewResults = () => {
    router.push("/leaderboard")
  }

  const handlePlayAgain = () => {
    router.push("/dashboard")
  }

  return (
    <div className="min-h-screen bg-gradient-to-br from-green-50 to-blue-100 dark:from-gray-900 dark:to-gray-800 flex items-center justify-center">
      <div className="container mx-auto px-4">
        <Card className="max-w-2xl mx-auto text-center">
          <CardHeader className="pb-6">
            <div className="mx-auto mb-4 p-4 bg-green-100 dark:bg-green-900/20 rounded-full w-fit">
              <Trophy className="h-12 w-12 text-green-600" />
            </div>
            <CardTitle className="text-3xl mb-2">Quiz abgeschlossen!</CardTitle>
            <p className="text-gray-600 dark:text-gray-300 text-lg">Großartige Arbeit, Team {currentTeam?.name}!</p>
          </CardHeader>
          <CardContent className="space-y-6">
            <div className="grid sm:grid-cols-2 gap-4">
              <div className="p-4 bg-blue-50 dark:bg-blue-900/20 rounded-lg">
                <CheckCircle className="h-8 w-8 text-blue-600 mx-auto mb-2" />
                <p className="font-semibold">Alle Fragen beantwortet</p>
                <p className="text-sm text-gray-600 dark:text-gray-400">Quiz erfolgreich abgeschlossen</p>
              </div>
              <div className="p-4 bg-purple-50 dark:bg-purple-900/20 rounded-lg">
                <Users className="h-8 w-8 text-purple-600 mx-auto mb-2" />
                <p className="font-semibold">Team-Leistung</p>
                <p className="text-sm text-gray-600 dark:text-gray-400">Antworten werden ausgewertet</p>
              </div>
            </div>

            <div className="pt-4 space-y-3">
              <Button onClick={handleViewResults} className="w-full" size="lg">
                Ergebnisse ansehen
              </Button>
              <Button variant="outline" onClick={handlePlayAgain} className="w-full bg-transparent">
                Zurück zum Dashboard
              </Button>
            </div>

            <div className="pt-4 border-t">
              <div className="flex items-center justify-center gap-2 text-sm text-gray-500">
                <AlertCircle className="h-4 w-4" />
                <span>Die Ergebnisse werden in Kürze aktualisiert</span>
              </div>
            </div>
          </CardContent>
        </Card>
      </div>
    </div>
  )
}
