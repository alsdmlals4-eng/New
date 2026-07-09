# AGENTS.md

## Project

Project name: `my little boat`
Engine: Godot 4.x
Language: GDScript
Genre: first-person healing drifting boat game

## Core Direction

The player sits in a small boat and watches the sea. The player body is not visible.

The game has no combat, no failure state, no competitive systems, no ads, and no payment system.

## MVP Rules

Core controls:
- Take Photo
- Appreciation Mode
- Speed Control

Core loop:
- Select today's mood
- Enter the sea scene
- Drift for 5 minutes
- Create voyage record
- Continue idle appreciation mode

Rewards:
- Companion affection
- Mood record
- Scenery collection
- Album-style collection

## Godot Rules

- Use Godot 4.x.
- Use GDScript unless explicitly requested otherwise.
- Prefer simple scene and node structures.
- Do not over-engineer.
- Do not add online systems unless explicitly requested.
- Do not add combat, stamina, HP, enemies, or failure systems.
- Keep UI mobile-friendly first.
- Support PC mouse input where it makes sense.
- Use clear node names and script names.
- Update README when adding setup or test steps.

## Suggested Scene Structure

- `scenes/main_menu.tscn`
- `scenes/game.tscn`
- `scenes/album.tscn`

## Suggested Script Areas

- `scripts/core/`
- `scripts/ui/`
- `scripts/voyage/`
- `scripts/companion/`
- `scripts/album/`

