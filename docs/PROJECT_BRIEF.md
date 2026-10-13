# TaskManager — Full-Stack Learning & Portfolio Project

> **Project type:** practical guided course + production-style portfolio application  
> **Duration:** 8 weeks, 2 hours a day (~112 hours)  
> **Start:** when the new local project is created  
> **Primary goal:** become ready for Junior C#/.NET Backend Developer interviews in Kraków, Poland, while demonstrating a working full-stack product.

## 1. Context for a new ChatGPT / Codex conversation

The learner already knows the basics of **C#, SQL (SQL Server/PostgreSQL), JavaScript, HTML and CSS**, has worked with **DELMIA Apriso / MES** at **Andea** (SQL, Process Builder, Web Components, enterprise processes), and has previously built a small `TaskManagerAPI` with REST CRUD, DTOs, and repository/service ideas. **This is a brand-new project, not a continuation or migration of the old TaskManagerAPI. Do not copy the old architecture or code automatically.** Treat the old project only as background experience.

The learner seeks paid employment in or remotely from Kraków. Priority role: **Junior .NET Backend Developer**, with adjacent SQL / MES / ERP / WMS opportunities also relevant. The product will be a local website with a real database, not just Swagger and API endpoints. It must be demonstrable to a recruiter.

### Guiding principles

1. **Teach, don't silently implement.** The learner should write important code and be able to explain it in an interview.
2. Give **small, time-boxed steps** with the purpose, expected result and verification criteria; avoid overwhelming code dumps.
3. Explain design trade-offs and ask the learner to attempt each core task before showing a full solution. Provide hints progressively.
4. Codex may inspect/edit/run files when authorized in the local project, but explain any significant code changes and let the learner review them.
5. Make real, working increments each day; finish with tests, a commit, and a short retrospective.
6. Don't introduce complicated architecture or tools just to impress. Prefer maintainable, idiomatic .NET code and a small coherent feature set.
7. The plan is adjustable: do not rush past misunderstood foundations to meet a calendar date.
8. Search contemporary documentation and validate versions/requirements before choosing exact package versions. Avoid unnecessary upgrades during a milestone.

## 2. Final product: TaskManager

A **full-stack team task management app**, run locally from a browser, backed by **ASP.NET Core Web API** and a **SQL database**. A recruiter can create accounts, manage projects and tasks, assign people, filter results, and see role-based behavior. The project should work from a clean clone using documented commands.

### MVP capabilities (mandatory)

- User registration and login; secure credential handling; protected routes.
- Projects: create, list, edit, archive/delete under explicit rules.
- Project members with simple roles: **Owner, Member** (optional later: Admin).
- Tasks: title, description, status (`Todo`, `InProgress`, `Done`), priority, due date, assigned user, timestamps, project association.
- Authorization: only permitted users may view or modify data; no cross-project data leaks.
- CRUD and validation for projects, tasks, memberships as appropriate.
- Search, filtering, sorting and server-side pagination for tasks.
- Browser interface: login/register, dashboard, project view, task list, task create/edit form, status controls, clear error and loading states.
- Database persistence across app restarts, EF Core migrations and seed/demo data (non-production only).
- API contract published through OpenAPI; clear HTTP codes and ProblemDetails errors.
- Unit and integration tests for meaningful business rules and authorization cases.
- Container-based local startup, build/test automation, onboarding documentation.

### Stretch goals — only after MVP

- Activity history / audit entries, comments, labels, board (Kanban) view, optimistic concurrency, notifications.
- Deployment to a public demo environment if feasible and safe.
- A small inventory/warehouse module **as a separate future project** if time allows, to connect the portfolio to MES/WMS experience. Do not undermine the TaskManager MVP for this.

### Not in scope initially

Microservices, Kubernetes, event brokers, complex DDD/CQRS, Redis, real-time collaboration, elaborate UI animations, mobile apps, payment flows. These can be discussed, not implemented by default.

## 3. Proposed technology stack

| Layer | Preferred choice | Why / notes |
|---|---|---|
| Backend | **C# / ASP.NET Core Web API**, currently supported .NET LTS chosen at kickoff | Hiring relevance and main educational focus |
| API style | REST, OpenAPI/Swagger UI if supported with appropriate tooling | Verifiable endpoints and recruiter demo |
| Persistence | **SQL Server + Entity Framework Core** | Matches existing SQL skills and common vacancies |
| Auth | **ASP.NET Core Identity** with cookie-based auth for same-origin UI *or* carefully implemented bearer tokens | Select at architecture kickoff; do not roll own password hashing; protect against CSRF if cookies used |
| Frontend | **React + TypeScript + Vite** | Modern practical UI; keep component architecture simple |
| Styling | Plain CSS / CSS Modules (optional Bootstrap) | Focus on usable UX, not a design framework learning detour |
| Tests | xUnit; ASP.NET Core `WebApplicationFactory`; frontend basic component tests if time | Backend testing priority |
| Local orchestration | Docker / Docker Compose | Reproducible backend + SQL Server (frontend either Vite dev server or container) |
| CI | GitHub Actions | Restore, build and test on push/PR |
| Git | GitHub repository, feature branches, focused commits | Portfolio transparency |

**Decision checkpoint:** Before implementing authentication, choose a coherent same-origin/cross-origin development strategy, token/cookie storage plan and security measures. Do not put long-lived credentials in `localStorage` by default. No secrets in commits. Apply only the complexity needed for a local learning app.

**Architecture (starting proposal, not dogma):**

```text
TaskManager/
├── backend/
│   ├── TaskManager.Api/             # Endpoints/controllers, auth, middleware, DI
│   ├── TaskManager.Application/     # Use cases, DTOs, validation, interfaces
│   ├── TaskManager.Domain/          # Core entities and rules
│   ├── TaskManager.Infrastructure/  # EF Core, DbContext, migrations, repositories as needed
│   └── tests/
│       ├── TaskManager.UnitTests/
│       └── TaskManager.IntegrationTests/
├── frontend/                        # React + TypeScript app
├── docs/                            # ERD, architecture, API examples, decisions
├── .github/workflows/
├── compose.yaml
├── README.md
└── PROJECT_BRIEF.md
```

Use the smallest sensible separation; repositories are **not mandatory** when EF Core already provides a suitable abstraction. Avoid layers with no meaningful responsibility.

## 4. Domain model (initial sketch)

- **User**: Id, display name, email, password-related fields managed by Identity.
- **Project**: Id, name, description, owner, created/updated timestamps, archived flag.
- **ProjectMembership**: UserId, ProjectId, Role, joined timestamp.
- **TaskItem** (avoid naming conflict with `System.Threading.Tasks.Task`): Id, ProjectId, Title, Description, Status, Priority, DueAt, AssignedUserId (nullable), CreatedByUserId, CreatedAt, UpdatedAt.

Discuss cardinality, foreign keys, indexes and cascading-delete policy before migrations. Ensure authorization is enforced **server-side**. Plan UTC storage for instants and localized display in UI. Pagination must be bounded and queries deterministic.

## 5. Eight-week course plan

**Daily rhythm (120 minutes):** 20 min theory/discussion → 80 min hands-on coding → 20 min tests, notes, commit, and verbal explanation. One day each week is used for consolidation/review. If the learner struggles, adjust scope rather than blindly pushing forward.

### Week 1 — Greenfield setup, C# refresh and first vertical slice

**Goal:** new repository, running API, running browser UI, clear structure, one end-to-end feature.

1. Define use cases, acceptance criteria, choose .NET version/tools; initialize repo and solution.
2. Refresh C# essentials: classes vs records, interfaces, DI, nullable types, collections; make tiny exercises.
3. Create ASP.NET Core API with a health endpoint, OpenAPI, error handling baseline.
4. Create React + TypeScript app; design navigation/layout and connect to health endpoint.
5. Implement a first temporary in-memory `GET/POST` tasks feature to learn request flow.
6. Learn async/await, HTTP verbs, status codes and DTO boundaries; refactor feature.
7. Review and demo: browser → API → response; README with startup commands; commit.

**Done when:** frontend and backend run locally; learner can explain HTTP request lifecycle, DI and API DTOs.

### Week 2 — Relational SQL and EF Core

**Goal:** real SQL Server persistence and core model.

1. Map users/projects/tasks/memberships and draft ER diagram.
2. Start local SQL Server; connect via EF Core `DbContext`.
3. Create first migrations, relationships and constraints.
4. Implement persisted project CRUD and task CRUD.
5. Practice SQL `JOIN`, `GROUP BY`, `HAVING`, aggregate queries and constraints.
6. Learn EF Core projections, tracking/`AsNoTracking`, `Include`, N+1, indexes.
7. Verify data survives restart; migration and seed workflow documented.

**Done when:** app persists and queries real data; learner can explain DbContext, migrations, relationships and indexes.

### Week 3 — Backend quality and complete task workflow

**Goal:** robust REST API with business logic.

1. Define task statuses, transitions and validation rules.
2. Implement application service/use cases and server-side validation.
3. Add filtering by project/status/assignee, search and sorting.
4. Add bounded pagination with stable ordering.
5. Add standardized errors with `ProblemDetails`; handle not-found and conflicts.
6. Add request logging and centralized exception handling; explore cancellation tokens.
7. End-to-end test manually from API client and frontend; review HTTP status codes.

**Done when:** invalid requests fail predictably and common queries work efficiently.

### Week 4 — Authentication, authorization, frontend integration

**Goal:** a coherent multi-user application.

1. Study auth vs authorization, claims, roles and basic OWASP concerns.
2. Configure ASP.NET Core Identity and registration/login/logout safely.
3. Define project membership/owner permissions and server-side policies.
4. Protect projects and tasks; test access denial between users.
5. Create frontend login/register forms, session handling and protected pages.
6. Connect project/task UI to authenticated endpoints; form errors, loading/empty states.
7. Security review: secrets, HTTPS in production, CORS, CSRF if cookies, no plaintext passwords.

**Done when:** user A cannot access user B's unauthorized projects/tasks, even by direct HTTP calls.

### Week 5 — Real usable frontend

**Goal:** a polished but restrained portfolio UI.

1. Define a simple visual system and responsive application shell.
2. Build project overview and project details screens.
3. Build task list with filter/search/sort/page controls.
4. Build task create/edit dialogs or pages with validation feedback.
5. Implement task status/priority updates and assignee selection.
6. Handle API errors, loading, empty states and keyboard/form accessibility.
7. User journey test: sign up → create project → invite/add member → assign/complete tasks.

**Done when:** a reviewer can use all essential workflows from the browser, without Swagger.

### Week 6 — Automated testing and code quality

**Goal:** prove correctness of important behavior.

1. Arrange–Act–Assert and unit-test basics with xUnit.
2. Unit tests for task validation and status rules.
3. Unit tests for membership and authorization-related business rules.
4. Integration tests with `WebApplicationFactory` and isolated test database strategy.
5. Test real endpoints for success, validation failure, 401/403/404 and data isolation.
6. Add frontend smoke/component tests if time, or prioritize complete browser flows.
7. Test review and refactor brittle code; measure meaningful coverage rather than target a vanity percentage.

**Done when:** minimum target **15 focused unit tests + 5 meaningful integration scenarios** (quality beats count); tests run repeatably.

### Week 7 — Docker, CI, performance and documentation

**Goal:** reproducible setup and professional project presentation.

1. Docker fundamentals: image/container, ports, volumes and environment variables.
2. Add multistage Dockerfile for backend.
3. Configure SQL Server via Compose, persistent volume, health readiness and development-only secrets.
4. Document frontend dev/production build startup and optional frontend container.
5. Configure GitHub Actions for restore/build/test (and frontend build if feasible).
6. Review SQL query plans, basic indexes, logging and data protection; fix one observed inefficiency.
7. Write README, architecture overview, ERD, screenshots/demo walkthrough, trade-offs.

**Done when:** a fresh clone can be brought up using documented steps and CI passes.

### Week 8 — Hardening, mock interviews and applying

**Goal:** demonstrable product and defendable skills.

1. Full bug bash, edge cases, security checks and accessibility review.
2. Refactor one weak spot with clear before/after explanation.
3. SQL interview session: joins, transactions, ACID, indexes, EF Core queries.
4. C# interview session: OOP, collections, LINQ, async/await, DI, exceptions.
5. Live coding: add one endpoint, query, test and basic UI usage within a time box.
6. Prepare CV bullets, concise portfolio description and 3–5 minute product walkthrough.
7. Final demo, release tag, retrospective and list of targeted job applications.

**Done when:** can explain technical trade-offs, implement a small feature unaided and show a working system in browser.

## 6. Daily session protocol for Codex

At the beginning of every day:

1. Read this brief, current repository state, `README.md`, recent commits and `docs/PROGRESS.md` (create this when project starts).
2. State: **today's goal**, why it matters to an interview, prerequisites, and a plan fitting **120 minutes**.
3. Teach the minimum theory (up to ~20 minutes). Give one concrete task at a time.
4. Let the learner try first. Offer a **hint → outline → full example** only as needed.
5. Review the learner's implementation: correctness, maintainability, security, SQL performance and test coverage.
6. Run appropriate tests, request a commit, and update progress notes.
7. Finish with **3 interview-style questions** from today's topic and a short self-assessment.

**Do not:** build the entire application in one autonomous pass; rewrite the learner's code without explanation; assume unfamiliar concepts are understood; claim tests passed without running them; require subscriptions to paid services.

### Progress file template (`docs/PROGRESS.md`)

```markdown
# Learning log
## Current phase
- Week / day:
- Current branch:
- Working features:
- Open issues / blockers:

## Session — YYYY-MM-DD
- Goal:
- Concepts learned:
- Implemented:
- Tests run / results:
- Decisions and why:
- What I can explain unaided:
- Next task:
```

## 7. Portfolio completion checklist

- [ ] New Git repository with meaningful commits and no secrets.
- [ ] Runnable ASP.NET Core API with OpenAPI description.
- [ ] Usable React + TypeScript website.
- [ ] SQL Server with EF Core migrations and meaningful relationships.
- [ ] Authenticated users, server-side project access controls.
- [ ] Project and task creation, updates, listing, filtering, sorting, pagination.
- [ ] Input validation, understandable errors, logs.
- [ ] Automated unit and integration tests.
- [ ] Docker-based repeatable local environment.
- [ ] Passing CI workflow.
- [ ] README with stack, architecture, features, instructions, screenshots and demo credentials only for disposable seed users.
- [ ] ERD and short architecture/decision notes.
- [ ] Clean browser demo and 3–5 minute explanation prepared.

## 8. Interview readiness gate

Consider the learner ready to actively interview when they can, **without AI assistance**:

- Explain C# OOP, interfaces vs abstract classes, value/reference types, LINQ, `IEnumerable` vs `IQueryable`, `async/await`, cancellation and DI lifetimes.
- Explain middleware, REST/HTTP status codes, DTO validation, authentication vs authorization and request lifecycle.
- Write SQL joins/aggregation, discuss indexes, transactions and common concurrency issues.
- Explain EF Core change tracking, migrations, projection, N+1 query problems.
- Add a small backend endpoint with validation, an EF query and at least one test within ~45 minutes.
- Debug one broken endpoint, interpret error logs and explain tests.
- Demo the full app, discuss a design trade-off, and admit limitations confidently.

These are readiness indicators, **not a promise of a job**. Begin applying to relevant junior and trainee positions from Week 1 while improving skills.

## 9. Job-search positioning

Primary: **Junior C#/.NET Backend Developer**. Secondary: **Junior SQL Developer**, **MES/ERP/WMS Application Developer**, **Technical Consultant** and junior full-stack with .NET. Highlight Andea/Apriso experience honestly; describe real responsibilities accurately, do not inflate titles or years. The new TaskManager project demonstrates modern .NET, persistence, business rules, testing, and UI integration. Prepare concise Polish and English explanations.

## 10. Immediate first instruction for the new project

> We are starting a **new TaskManager from zero**. First inspect the local directory and tooling. Do not generate the whole application. Propose the Week 1 / Day 1 session for 2 hours: agree on the MVP, review installed .NET/Node/Docker versions, initialize the Git repository and solution, run a minimal API and React frontend, and explain every step so I can reproduce it. Track progress in `docs/PROGRESS.md`. Ask me to implement core steps before writing them for me.

---

**Definition of success:** not maximum feature count; a small, secure, tested, reproducible full-stack project whose code and decisions the learner genuinely understands and can defend in a junior .NET interview.
