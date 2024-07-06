# Vivelin.net

This repository hosts the code behind the upcoming version of my personal
website, [Vivelin.net]. Currently still a Razor Pages ASP.NET Core app, I’m
rebuilding it in [Remix] with an ASP.NET Core API backend to make it easier to
add both client-side functionality and new content.

## Quick start

Run the dev server:

```sh
bun run dev
```

Or, in Visual Studio Code, **Terminal > Run Task... > bun > bun: dev** (with the
Bun for Visual Studio Code extension).

## Deployment

First, build your app for production:

```sh
bun run build
```

Then run the app in production mode:

```sh
bun start
```

[Vivelin.net]: https://vivelin.net/
[Remix]: https://remix.run/

