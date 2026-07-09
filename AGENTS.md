# AGENTS.md

Codex and other coding agents should follow this file when working in this repository.

## Project

Project name: `my little boat`
Engine: Godot 4.7 stable
Language: GDScript
Genre: first-person healing drifting boat game

The player sits in a small boat and watches the sea. The player body is not visible.

Do not add combat, failure states, competitive systems, ads, payments, or online letter sharing.

## Core Game Direction

Core controls:
- Take Photo
- Appreciation Mode
- Speed Control

Core loop:
- Select today's mood.
- Enter the sea scene.
- Drift for 5 minutes.
- Create today's voyage record.
- Continue idle appreciation mode.

Rewards:
- Companion affection.
- Mood record.
- Scenery collection.
- Album-style collection.

## Work Style

- Inspect the actual files before editing. Use `rg` or Godot project structure instead of relying on memory.
- State assumptions when the request is ambiguous. Ask before making risky product or architecture decisions.
- Prefer the smallest change that solves the request.
- Match the existing scene, node, script, and naming style.
- Avoid speculative abstractions, large rewrites, or broad cleanup.
- Do not revert, overwrite, or reformat unrelated user changes.
- If the worktree is dirty, understand whether the dirty files are related before editing them.
- Read real error messages and logs before applying a fix.

## Godot Rules

- Use Godot 4.7 stable.
- Use GDScript unless explicitly requested otherwise.
- Keep scene and node structures simple.
- Use clear node names such as `MoodStatusLabel`, `TakePhotoButton`, and `AlbumView`.
- Keep UI mobile-friendly first, with PC mouse input where it makes sense.
- Prefer local-first systems before cloud sync, login, or online features.
- Do not add combat, stamina, HP, enemies, damage, death, failure conditions, or ranking systems.
- Do not add paid assets or dependency-heavy plugins without explicit approval.
- Update `README.md` when setup, controls, scenes, or test steps change.

## File Header Comments

For new source files, add a one-line Korean comment at the top explaining the file's role.

GDScript example:

```gdscript
# 항해 화면의 기본 상호작용을 관리한다.
extends Control
```

Skip header comments for generated files, Godot scene files, `.import` files, lockfiles, and simple README placeholders.

## Planning

For small tasks, work directly after reading the relevant files.

For larger tasks that touch multiple scenes, scripts, or gameplay systems, briefly state:
- What will change.
- Which files or scenes are likely involved.
- How the change will be verified.

Create `checklist.md` or `context-notes.md` only for long-running, risky, or multi-session work. Do not create extra process files for small, self-contained changes.

## Verification

If code, scenes, or project settings changed, run the smallest useful Godot check before marking the task complete.

Preferred checks:

```powershell
godot --headless --path . --quit
godot --headless --path . --scene "res://scenes/main_menu.tscn" --quit-after 1
```

Known Windows local fallback in this workspace:

```powershell
& "C:\Users\user\Downloads\Godot_v4.7-stable_win64.exe\Godot_v4.7-stable_win64_console.exe" --headless --path . --quit
```

For UI-only or documentation-only changes, explain what was inspected instead of claiming gameplay was tested.

Final replies should include:
- What changed.
- What was verified.
- Any remaining risk or manual Godot check the user should perform.

## Commit Guidance

Commit only when one logical change is complete and the repository workflow expects it.

Good commit examples:
- `Improve AGENTS Godot guidance`
- `Add mood selection UI`
- `Fix album back button flow`

Do not mix unrelated gameplay, UI, documentation, and cleanup changes in one commit unless the user explicitly asks for a broad conversion.

## Korean Output

When replying to a Korean user, answer in Korean.

Avoid ending Korean prose lines with a colon. Prefer a period, question mark, or exclamation mark. Colons are fine in code, paths, key-value examples, timestamps, and Markdown labels.

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
