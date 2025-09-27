import type React from "react"
import type { Metadata } from "next"

import "./globals.css"
import ClientLayout from "./client-layout"

export const metadata: Metadata = {
  title: "Rätselrausch",
  description: "Tauche ab in den Rätselrauch",
  generator: "Rätselrausch",
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
