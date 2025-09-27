import type React from "react"
import type { Metadata } from "next"

import "./globals.css"
import ClientLayout from "./client-layout"

export const metadata: Metadata = {
  title: "v0 App",
  description: "Created with v0",
  generator: "v0.app",
}

export default function RootLayout({
  children,
}: {
  children: React.ReactNode
}) {
  return (
    <html lang="en" className="antialiased">
      <body className={`font-sans`}>
        <ClientLayout>{children}</ClientLayout>
      </body>
    </html>
  )
}
