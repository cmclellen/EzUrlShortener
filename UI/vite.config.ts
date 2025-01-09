import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";

// https://vite.dev/config/
export default defineConfig({
  plugins: [react()],
  server: {
    cors: true,
    proxy: {
      "/api": {
        target: "https://localhost:7231",
        secure: false,
        changeOrigin: true,
        // rewrite: (path) => {
        //   console.log(path);
        //   return path.replace(/^\/api/, "");
        // },
      },
    },
  },
});
