/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./Views/**/*.cshtml",
    "./wwwroot/**/*.html",
    "./wwwroot/**/*.js"
  ],
  theme: {
    extend: {
      colors: {
        primary: {
          light: '#d4ff33', // Lighter neon
          DEFAULT: '#C6FF00', // Neon Lemon (Accent)
          dark: '#a3d100', // Darker neon
        },
        background: '#0D0D0D', // Main Background
        backgroundAlt: '#121212', // Alternate background if needed
        surface: '#1E1E1E', // Cards
        surfaceHover: '#2A2A2A', // Card Hover
        textMain: '#FFFFFF',
        textMuted: '#B0B0B0',
      },
      fontFamily: {
        sans: ['Inter', 'sans-serif'],
      }
    },
  },
  plugins: [],
}
