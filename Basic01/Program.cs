using System;
using System.Collections.Generic;

using SDL2;

//from MetaCipher/sdl-2.0-basics

namespace TestSdl01
{
    static class Program
    {
        static bool running = true;
        static IntPtr winPtr;
        static IntPtr rendererPtr;


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            if (!Init())
                return;
            SDL.SDL_Event sdlEvent;

            while (running)
            {
                while (SDL.SDL_PollEvent(out sdlEvent) != 0)
                {
                    OnEvent(sdlEvent);
                    if (sdlEvent.type == SDL.SDL_EventType.SDL_QUIT)
                    {
                        running = false;
                        break;
                    }
                    Loop();
                    Render();
                    SDL.SDL_Delay(10);
                }
            }
        }
        static void OnEvent(SDL.SDL_Event sdlEvent)
        {
            if (sdlEvent.type == SDL.SDL_EventType.SDL_MOUSEBUTTONDOWN)
            {
                SDL.SDL_SetWindowTitle(winPtr, "OKOK!");
            }
        }
        static void Loop()
        {

        }
        static void Render()
        {

            SDL.SDL_RenderClear(rendererPtr);
            SDL.SDL_RenderPresent(rendererPtr);

        }
        static void CleanUp()
        {
            if (rendererPtr != IntPtr.Zero)
            {
                SDL.SDL_DestroyRenderer(rendererPtr);
                rendererPtr = IntPtr.Zero;
            }
            if (winPtr != IntPtr.Zero)
            {
                SDL.SDL_DestroyWindow(winPtr);
                winPtr = IntPtr.Zero;
            }
            SDL.SDL_Quit();
        }
        static bool Init()
        {
            //1. 
            if (SDL.SDL_Init(SDL.SDL_INIT_VIDEO) < 0)
            {

                Console.WriteLine("unable to init SDL");
                return false;
            }
            //2.
            if (SDL.SDL_SetHint(SDL.SDL_HINT_RENDER_SCALE_QUALITY, "1") == 0)
            {
                Console.WriteLine("unable to init hint");
                return false;
            }
            //3.

            winPtr = SDL.SDL_CreateWindow("My SDL Game", 200, 50, 800, 600, SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);

            var primarySurface = SDL.SDL_GetWindowSurface(winPtr);

            rendererPtr = SDL.SDL_CreateRenderer(winPtr, -1, (uint)SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED);
            if (rendererPtr == IntPtr.Zero)
            {
                //error

                return false;
            }
            SDL.SDL_SetRenderDrawColor(rendererPtr, 0x00, 0x00, 0x00, 0xFF);

            return true;
        }
    }
}
