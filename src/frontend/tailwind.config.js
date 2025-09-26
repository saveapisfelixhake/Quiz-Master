/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./app/**/*.{js,ts,jsx,tsx,mdx}",   // if using /app dir
    "./pages/**/*.{js,ts,jsx,tsx,mdx}", // if using /pages dir
    "./components/**/*.{js,ts,jsx,tsx,mdx}",
  ],
  theme: {
    extend: {
        
    },
    colors:{
        fmi: "#5b2e49"
    }
  },
  plugins: [],
}
