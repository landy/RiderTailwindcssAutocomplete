# Repro for issue with TailwindCSS autocomplete in F# code

## Steps to Reproduce

1. run `dotnet tool restore`
2. run `dotnet paket install`
3. run `yarn install`
4. use tailwindCSS plugin config in rider with the default version of the tailwindcss language server (It does not work with the latest 0.14.15 version either).
```
{
  "includeLanguages": {
    "ftl": "html",
    "jinja": "html",
    "jinja2": "html",
    "smarty": "html",
    "tmpl": "gohtml",
    "cshtml": "html",
    "vbhtml": "html",
    "razor": "html",
    "fsharp": "html"
  },
  "files": {
    "exclude": [
      "**/.git/**",
      "**/node_modules/**",
      "**/.hg/**",
      "**/.svn/**"
    ]
  },
  "emmetCompletions": false,
  "classAttributes": ["class", "className", "ngClass"],
  "colorDecorators": true,
  "showPixelEquivalents": true,
  "rootFontSize": 16,
  "hovers": true,
  "suggestions": true,
  "codeActions": true,
  "validate": true,
  "lint": {
    "invalidScreen": "error",
    "invalidVariant": "error",
    "invalidTailwindDirective": "error",
    "invalidApply": "error",
    "invalidConfigPath": "error",
    "cssConflict": "warning",
    "recommendedVariantOrder": "warning"
  },
  "experimental": {
    "configFile": null,
    "classRegex": [
      "prop\\.className\\s*\"([^\"]*)\"",
      "Html\\.divClassed\\s*\"([^\"]*)\""
    ]
  }
}
```
I added these values:

```"fsharp": "html"```

and

```
"classRegex": [
  "prop\\.className\\s*\"([^\"]*)\"",
  "Html\\.divClassed\\s*\"([^\"]*)\""
]
```
5. open `src/TailwindAutocomplete.Client/View.fs`

In this file there are two flex classes used. If you hover over them, you should sees class definitions coming from TailwindCSS language server.
If you try to add more classes there is no autocomplete.

This was working in the `Rider 2024.3.7`.

