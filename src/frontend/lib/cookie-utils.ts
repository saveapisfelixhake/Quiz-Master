export interface CookiePlayer {
  id: string
  name: string
  avatar?: string
  teamId: string
  joinedAt: string 
}

export interface CookieTeam {
  id: string
  name: string
  code: string
  score: number
  createdAt: string 
}

export const COOKIE_KEYS = {
  PLAYER: "quiz_player",
  TEAM: "quiz_team",
} as const

export function setCookie(name: string, value: string, days = 30): void {
  const expires = new Date()
  expires.setTime(expires.getTime() + days * 24 * 60 * 60 * 1000)
  document.cookie = `${name}=${value};expires=${expires.toUTCString()};path=/;SameSite=Lax`
}

export function getCookie(name: string): string | null {
  if (typeof document === "undefined") return null

  const nameEQ = name + "="
  const ca = document.cookie.split(";")

  for (let i = 0; i < ca.length; i++) {
    let c = ca[i]
    while (c.charAt(0) === " ") c = c.substring(1, c.length)
    if (c.indexOf(nameEQ) === 0) return c.substring(nameEQ.length, c.length)
  }
  return null
}

export function deleteCookie(name: string): void {
  document.cookie = `${name}=;expires=Thu, 01 Jan 1970 00:00:00 UTC;path=/;`
}

export function savePlayerToCookie(player: CookiePlayer): void {
  setCookie(COOKIE_KEYS.PLAYER, JSON.stringify(player))
}

export function getPlayerFromCookie(): CookiePlayer | null {
  const playerData = getCookie(COOKIE_KEYS.PLAYER)
  if (!playerData) return null

  try {
    return JSON.parse(playerData)
  } catch {
    return null
  }
}

export function saveTeamToCookie(team: CookieTeam): void {
  setCookie(COOKIE_KEYS.TEAM, JSON.stringify(team))
}

export function getTeamFromCookie(): CookieTeam | null {
  const teamData = getCookie(COOKIE_KEYS.TEAM)
  if (!teamData) return null

  try {
    return JSON.parse(teamData)
  } catch {
    return null
  }
}

export function clearPlayerCookies(): void {
  deleteCookie(COOKIE_KEYS.PLAYER)
  deleteCookie(COOKIE_KEYS.TEAM)
}
