"use client"

import type React from "react"


import { useEffect } from "react"
import { useQuizStore } from "@/lib/quiz-store"
import { Suspense } from "react"

function StoreInitializer() {
  const initializeFromCookies = useQuizStore((state) => state.initializeFromCookies)

  useEffect(() => {
    initializeFromCookies()
  }, [initializeFromCookies])

  return null
}

export default function ClientLayout({
  children,
}: {
  children: React.ReactNode
}) {
  return (
    <>
      <Suspense fallback={null}>
        <StoreInitializer />
        {children}
      </Suspense>
    </>
  )
}

export { ClientLayout }
