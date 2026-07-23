# Deucarian UI Binding Agent Notes

Package ID: `com.deucarian.ui-binding`
Repository: `Deucarian/UI-Binding`

Follow the canonical Deucarian governance docs in [Package Registry](https://github.com/Deucarian/Package-Registry/blob/main/ARCHITECTURE.md), especially capability ownership and dependency rules.

## Ownership

This package owns:

- Collection-to-UI synchronization and generated item binding.

Registered capabilities:
- `ui-binding`

This package must not own:

- Routing/navigation, Core State ownership, world selection, or copied cleanup helpers.

## Dependencies

Allowed dependency shape:

- May depend on Common for generated item cleanup and UGUI when directly used.

Required dependencies and why:

- `com.deucarian.common`: approved Unity object lifetime helper.
- `com.unity.ugui`: UGUI package used by UI adapters.

Optional/version-defined dependencies:

- None.

Architecture exceptions:

- None.

## Policies

- Logging: Do not add Logging unless production UI binding code directly logs through the facade.
- Common: Use `UnityObjectUtility.DestroySafely` for generated item cleanup.
- Editor UI: No editor shell ownership.
- Diagnostics: No diagnostics ownership.
- Testing: Tests should cover binding behavior without adding routing responsibilities.

## Validation

Run the shared validator before committing:

```powershell
python C:/Repositories/Package-Registry/Tools/deucarian_package_validator.py --registry-root C:/Repositories/Package-Registry --repository-root . --config deucarian-package.json
```

Also run existing repository tests when changing code or asmdefs. Documentation-only updates should still run `git diff --check`.

## Codex Guidance

- Inspect current files before changing anything.
- Work on `develop`; do not edit or merge `main` unless the task is promotion-only.
- Do not edit `Library/PackageCache`.
- Do not guess package versions or dependency versions.
- Do not add package dependencies casually; update asmdefs, `package.json`, `deucarian-package.json`, Package Registry, and fallback catalogs together when a dependency is truly required.
- Do not create local copies of shared helpers.
- Keep commits focused and report exactly what changed and what was validated.

## Before Adding Code

- Confirm the change fits this package's ownership boundary.
- Reuse existing local patterns and helpers.
- Avoid broad refactors without audit support.
- Preserve runtime/editor behavior unless the task explicitly asks to change it.

## Before Adding A Dependency

- Is the capability already owned by that package?
- Is it used by production code, editor code, sample code, or tests?
- Does the asmdef reference match `package.json`?
- Does `deucarian-package.json` need updating?
- Does Package Registry need updating?
- Does Package Installer fallback catalog need updating?
- Does Bootstrap fallback catalog need updating?
- Are exact versions propagated without guessing?

## Before Adding A Helper

- Is this package the capability owner?
- Is this behavior repeated in at least three production packages?
- Is there an existing owner package?
- Should this remain local?
- Has the audit been updated?

## Debug And Unity Object Lifetime

- Do not add direct Unity Debug calls. Add Logging only if this package directly needs logging and governance approves the dependency.
- Production Unity object cleanup must use Common `UnityObjectUtility.DestroySafely`; do not copy the helper locally.
- Test fixture teardown may use `DestroyImmediate` directly.
