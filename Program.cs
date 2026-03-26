using System;
using System.Collections.Generic;
using System.Linq;

namespace FitTrackAPI
{
    // =====================================================
    // ENUMS
    // =====================================================
    public enum WorkoutType { Yoga, Strength, Cardio, Hiit, Pilates, Crossfit }
    public enum DifficultyLevel { Beginner, Intermediate, Advanced }

    // =====================================================
    // ENTITIES
    // =====================================================
    public class Workout
    {
        public Guid WorkoutId { get; set; }
        public Guid TrainerId { get; set; }
        public string Title { get; set; }
        public WorkoutType WorkoutType { get; set; }
        public DifficultyLevel Difficulty { get; set; }
        public int DurationMinutes { get; set; }
        public string Description { get; set; }
        public int? CaloriesBurnEstimate { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
    }

    public class Trainer
    {
        public Guid TrainerId { get; set; }
        public string FullName { get; set; }
        public string Specialization { get; set; }
        public int ExperienceYears { get; set; }
        public decimal Rating { get; set; }
    }

    public class Client
    {
        public Guid ClientId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string MembershipType { get; set; }
        public DateTime? MembershipExpiresAt { get; set; }
    }

    // =====================================================
    // DTOs (Data Transfer Objects)
    // =====================================================
    public class WorkoutDto
    {
        public Guid WorkoutId { get; set; }
        public Guid TrainerId { get; set; }
        public string TrainerName { get; set; }
        public string Title { get; set; }
        public string WorkoutType { get; set; }
        public string Difficulty { get; set; }
        public int DurationMinutes { get; set; }
        public string Description { get; set; }
        public int? CaloriesBurnEstimate { get; set; }
        public bool IsActive { get; set; }
    }

    public class CreateWorkoutDto
    {
        public Guid TrainerId { get; set; }
        public string Title { get; set; }
        public string WorkoutType { get; set; }
        public string Difficulty { get; set; }
        public int DurationMinutes { get; set; }
        public string Description { get; set; }
        public int? CaloriesBurnEstimate { get; set; }
    }

    public class UpdateWorkoutDto
    {
        public string Title { get; set; }
        public string WorkoutType { get; set; }
        public string Difficulty { get; set; }
        public int DurationMinutes { get; set; }
        public string Description { get; set; }
        public int? CaloriesBurnEstimate { get; set; }
        public bool IsActive { get; set; }
    }

    // =====================================================
    // SERVICE LAYER
    // =====================================================
    public interface IFitTrackService
    {
        List<WorkoutDto> GetAllWorkouts();
        WorkoutDto GetWorkoutById(Guid id);
        WorkoutDto CreateWorkout(CreateWorkoutDto dto);
        bool UpdateWorkout(Guid id, UpdateWorkoutDto dto);
        bool DeleteWorkout(Guid id);
        List<WorkoutDto> GetWorkoutsByType(string type);
        List<WorkoutDto> GetWorkoutsByDifficulty(string difficulty);
        List<WorkoutDto> GetWorkoutsByTrainer(Guid trainerId);
        List<Trainer> GetAllTrainers();
        Trainer GetTrainerById(Guid id);
        List<Client> GetAllClients();
        object GetStatistics();
    }

    public class FitTrackService : IFitTrackService
    {
        private readonly List<Workout> _workouts = new();
        private readonly List<Trainer> _trainers = new();
        private readonly List<Client> _clients = new();

        public FitTrackService()
        {
            SeedData();
        }

        private void SeedData()
        {
            var trainer1 = new Trainer
            {
                TrainerId = Guid.NewGuid(),
                FullName = "Іван Тренер",
                Specialization = "йога, пілатес, силові",
                ExperienceYears = 5,
                Rating = 4.8m
            };
            var trainer2 = new Trainer
            {
                TrainerId = Guid.NewGuid(),
                FullName = "Марія Фітнес",
                Specialization = "кардіо, HIIT, функціональний тренінг",
                ExperienceYears = 3,
                Rating = 4.9m
            };
            _trainers.Add(trainer1);
            _trainers.Add(trainer2);

            _clients.Add(new Client
            {
                ClientId = Guid.NewGuid(),
                FullName = "Олександр Петренко",
                Email = "olexandr@example.com",
                MembershipType = "Річний",
                MembershipExpiresAt = DateTime.UtcNow.AddMonths(8)
            });
            _clients.Add(new Client
            {
                ClientId = Guid.NewGuid(),
                FullName = "Олена Шевченко",
                Email = "olena@example.com",
                MembershipType = "Місячний",
                MembershipExpiresAt = DateTime.UtcNow.AddDays(15)
            });

            _workouts.Add(new Workout
            {
                WorkoutId = Guid.NewGuid(),
                TrainerId = trainer1.TrainerId,
                Title = "Ранкова йога",
                WorkoutType = WorkoutType.Yoga,
                Difficulty = DifficultyLevel.Beginner,
                DurationMinutes = 60,
                Description = "Ранкова практика для початківців. Розслаблення та розтяжка.",
                CaloriesBurnEstimate = 250,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            });
            _workouts.Add(new Workout
            {
                WorkoutId = Guid.NewGuid(),
                TrainerId = trainer1.TrainerId,
                Title = "Силове тренування",
                WorkoutType = WorkoutType.Strength,
                Difficulty = DifficultyLevel.Intermediate,
                DurationMinutes = 45,
                Description = "Інтенсивне силове тренування для всього тіла",
                CaloriesBurnEstimate = 400,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            });
            _workouts.Add(new Workout
            {
                WorkoutId = Guid.NewGuid(),
                TrainerId = trainer2.TrainerId,
                Title = "Кардіо марафон",
                WorkoutType = WorkoutType.Cardio,
                Difficulty = DifficultyLevel.Advanced,
                DurationMinutes = 30,
                Description = "Високоінтенсивне кардіо для спалювання жиру",
                CaloriesBurnEstimate = 350,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            });
            _workouts.Add(new Workout
            {
                WorkoutId = Guid.NewGuid(),
                TrainerId = trainer2.TrainerId,
                Title = "HIIT Інтенсив",
                WorkoutType = WorkoutType.Hiit,
                Difficulty = DifficultyLevel.Advanced,
                DurationMinutes = 25,
                Description = "Високоінтенсивний інтервальний тренінг",
                CaloriesBurnEstimate = 450,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            });
            _workouts.Add(new Workout
            {
                WorkoutId = Guid.NewGuid(),
                TrainerId = trainer1.TrainerId,
                Title = "Пілатес для спини",
                WorkoutType = WorkoutType.Pilates,
                Difficulty = DifficultyLevel.Beginner,
                DurationMinutes = 50,
                Description = "Зміцнення м'язів спини та кора",
                CaloriesBurnEstimate = 200,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            });
        }

        private WorkoutDto MapToDto(Workout w)
        {
            var trainer = _trainers.FirstOrDefault(t => t.TrainerId == w.TrainerId);
            return new WorkoutDto
            {
                WorkoutId = w.WorkoutId,
                TrainerId = w.TrainerId,
                TrainerName = trainer?.FullName ?? "Невідомий",
                Title = w.Title,
                WorkoutType = w.WorkoutType.ToString(),
                Difficulty = w.Difficulty.ToString(),
                DurationMinutes = w.DurationMinutes,
                Description = w.Description,
                CaloriesBurnEstimate = w.CaloriesBurnEstimate,
                IsActive = w.IsActive
            };
        }

        public List<WorkoutDto> GetAllWorkouts()
        {
            return _workouts.Select(MapToDto).ToList();
        }

        public WorkoutDto GetWorkoutById(Guid id)
        {
            var workout = _workouts.FirstOrDefault(w => w.WorkoutId == id);
            return workout != null ? MapToDto(workout) : null;
        }

        public WorkoutDto CreateWorkout(CreateWorkoutDto dto)
        {
            if (dto.DurationMinutes <= 0)
                throw new ArgumentException("Тривалість має бути додатньою.");

            if (!Enum.TryParse<WorkoutType>(dto.WorkoutType, true, out var workoutType))
                throw new ArgumentException("Невірний тип тренування.");
            if (!Enum.TryParse<DifficultyLevel>(dto.Difficulty, true, out var difficulty))
                throw new ArgumentException("Невірний рівень складності.");

            var workout = new Workout
            {
                WorkoutId = Guid.NewGuid(),
                TrainerId = dto.TrainerId,
                Title = dto.Title,
                WorkoutType = workoutType,
                Difficulty = difficulty,
                DurationMinutes = dto.DurationMinutes,
                Description = dto.Description,
                CaloriesBurnEstimate = dto.CaloriesBurnEstimate,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };
            _workouts.Add(workout);
            return MapToDto(workout);
        }

        public bool UpdateWorkout(Guid id, UpdateWorkoutDto dto)
        {
            var existing = _workouts.FirstOrDefault(w => w.WorkoutId == id);
            if (existing == null) return false;

            existing.Title = dto.Title ?? existing.Title;
            existing.DurationMinutes = dto.DurationMinutes;
            existing.Description = dto.Description ?? existing.Description;
            existing.CaloriesBurnEstimate = dto.CaloriesBurnEstimate;
            existing.IsActive = dto.IsActive;

            if (!string.IsNullOrEmpty(dto.WorkoutType) && Enum.TryParse<WorkoutType>(dto.WorkoutType, true, out var wt))
                existing.WorkoutType = wt;
            if (!string.IsNullOrEmpty(dto.Difficulty) && Enum.TryParse<DifficultyLevel>(dto.Difficulty, true, out var dl))
                existing.Difficulty = dl;

            return true;
        }

        public bool DeleteWorkout(Guid id)
        {
            var workout = _workouts.FirstOrDefault(w => w.WorkoutId == id);
            return workout != null && _workouts.Remove(workout);
        }

        public List<WorkoutDto> GetWorkoutsByType(string type)
        {
            if (!Enum.TryParse<WorkoutType>(type, true, out var wt))
                return new List<WorkoutDto>();
            return _workouts.Where(w => w.WorkoutType == wt).Select(MapToDto).ToList();
        }

        public List<WorkoutDto> GetWorkoutsByDifficulty(string difficulty)
        {
            if (!Enum.TryParse<DifficultyLevel>(difficulty, true, out var dl))
                return new List<WorkoutDto>();
            return _workouts.Where(w => w.Difficulty == dl).Select(MapToDto).ToList();
        }

        public List<WorkoutDto> GetWorkoutsByTrainer(Guid trainerId)
        {
            return _workouts.Where(w => w.TrainerId == trainerId).Select(MapToDto).ToList();
        }

        public List<Trainer> GetAllTrainers() => _trainers;
        public Trainer GetTrainerById(Guid id) => _trainers.FirstOrDefault(t => t.TrainerId == id);
        public List<Client> GetAllClients() => _clients;

        public object GetStatistics()
        {
            return new
            {
                TotalWorkouts = _workouts.Count,
                ActiveWorkouts = _workouts.Count(w => w.IsActive),
                TotalTrainers = _trainers.Count,
                TotalClients = _clients.Count,
                WorkoutsByType = Enum.GetValues(typeof(WorkoutType))
                    .Cast<WorkoutType>()
                    .Select(t => new { Type = t.ToString(), Count = _workouts.Count(w => w.WorkoutType == t) })
                    .Where(x => x.Count > 0),
                WorkoutsByDifficulty = Enum.GetValues(typeof(DifficultyLevel))
                    .Cast<DifficultyLevel>()
                    .Select(d => new { Difficulty = d.ToString(), Count = _workouts.Count(w => w.Difficulty == d) })
                    .Where(x => x.Count > 0),
                AverageDuration = _workouts.Average(w => w.DurationMinutes),
                AverageCalories = _workouts.Where(w => w.CaloriesBurnEstimate.HasValue)
                                           .Average(w => w.CaloriesBurnEstimate)
            };
        }
    }

    // =====================================================
    // CONSOLE UI
    // =====================================================
    class Program
    {
        static IFitTrackService service = new FitTrackService();

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Clear();

            Console.WriteLine("╔════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                      FITTRACK API                              ║");
            Console.WriteLine("║                  Система управління тренуваннями               ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════════╝");

            while (true)
            {
                Console.WriteLine("\n┌─────────────────────────────────────────────────────────────┐");
                Console.WriteLine("│                      ГОЛОВНЕ МЕНЮ                            │");
                Console.WriteLine("├─────────────────────────────────────────────────────────────┤");
                Console.WriteLine("│  1.  Показати всі тренування                                │");
                Console.WriteLine("│  2.  Знайти тренування за ID                                │");
                Console.WriteLine("│  3.  Додати нове тренування                                 │");
                Console.WriteLine("│  4.  Оновити тренування                                     │");
                Console.WriteLine("│  5.  Видалити тренування                                    │");
                Console.WriteLine("│  6.  Фільтр за типом тренування                             │");
                Console.WriteLine("│  7.  Фільтр за рівнем складності                            │");
                Console.WriteLine("│  8.  Фільтр за тренером                                     │");
                Console.WriteLine("│  9.  Список тренерів                                        │");
                Console.WriteLine("│ 10.  Список клієнтів                                        │");
                Console.WriteLine("│ 11.  Статистика                                             │");
                Console.WriteLine("│  0.  Вийти                                                  │");
                Console.WriteLine("└─────────────────────────────────────────────────────────────┘");
                Console.Write("\n👉 Оберіть опцію: ");

                var choice = Console.ReadLine();
                Console.Clear();

                switch (choice)
                {
                    case "1": ShowAllWorkouts(); break;
                    case "2": FindWorkoutById(); break;
                    case "3": CreateWorkout(); break;
                    case "4": UpdateWorkout(); break;
                    case "5": DeleteWorkout(); break;
                    case "6": FilterByType(); break;
                    case "7": FilterByDifficulty(); break;
                    case "8": FilterByTrainer(); break;
                    case "9": ShowAllTrainers(); break;
                    case "10": ShowAllClients(); break;
                    case "11": ShowStatistics(); break;
                    case "0":
                        Console.WriteLine("\n✨ Дякуємо за використання FitTrack API! До побачення!\n");
                        return;
                    default:
                        Console.WriteLine("❌ Невірний вибір. Спробуйте ще раз.\n");
                        break;
                }

                Console.WriteLine("\nНатисніть Enter для продовження...");
                Console.ReadLine();
                Console.Clear();
            }
        }

        static void ShowAllWorkouts()
        {
            var workouts = service.GetAllWorkouts();
            Console.WriteLine("\n════════════════════════════════════════════════════════════════");
            Console.WriteLine("                      СПИСОК ТРЕНУВАНЬ");
            Console.WriteLine("════════════════════════════════════════════════════════════════\n");

            if (workouts.Count == 0)
            {
                Console.WriteLine("📭 Немає доступних тренувань.\n");
                return;
            }

            foreach (var w in workouts)
            {
                Console.WriteLine($"┌─────────────────────────────────────────────────────────────┐");
                Console.WriteLine($"│ 🏋️  {w.Title}");
                Console.WriteLine($"├─────────────────────────────────────────────────────────────┤");
                Console.WriteLine($"│ ID:         {w.WorkoutId}");
                Console.WriteLine($"│ Тренер:     {w.TrainerName}");
                Console.WriteLine($"│ Тип:        {w.WorkoutType}");
                Console.WriteLine($"│ Складність: {w.Difficulty}");
                Console.WriteLine($"│ Тривалість: {w.DurationMinutes} хв.");
                Console.WriteLine($"│ Калорії:    {(w.CaloriesBurnEstimate.HasValue ? w.CaloriesBurnEstimate + " ккал" : "не вказано")}");
                Console.WriteLine($"│ Статус:     {(w.IsActive ? "✅ Активне" : "❌ Неактивне")}");
                Console.WriteLine($"│ Опис:       {w.Description}");
                Console.WriteLine($"└─────────────────────────────────────────────────────────────┘\n");
            }
        }

        static void FindWorkoutById()
        {
            Console.Write("\n🔍 Введіть ID тренування: ");
            if (Guid.TryParse(Console.ReadLine(), out var id))
            {
                var workout = service.GetWorkoutById(id);
                if (workout != null)
                {
                    Console.WriteLine("\n════════════════════════════════════════════════════════════════");
                    Console.WriteLine($"🏋️  {workout.Title}");
                    Console.WriteLine("════════════════════════════════════════════════════════════════");
                    Console.WriteLine($" ID:         {workout.WorkoutId}");
                    Console.WriteLine($" Тренер:     {workout.TrainerName}");
                    Console.WriteLine($" Тип:        {workout.WorkoutType}");
                    Console.WriteLine($" Складність: {workout.Difficulty}");
                    Console.WriteLine($" Тривалість: {workout.DurationMinutes} хв.");
                    Console.WriteLine($" Калорії:    {(workout.CaloriesBurnEstimate.HasValue ? workout.CaloriesBurnEstimate + " ккал" : "не вказано")}");
                    Console.WriteLine($" Статус:     {(workout.IsActive ? "Активне" : "Неактивне")}");
                    Console.WriteLine($" Опис:       {workout.Description}");
                    Console.WriteLine("════════════════════════════════════════════════════════════════\n");
                }
                else
                {
                    Console.WriteLine($"\n❌ Тренування з ID {id} не знайдено.\n");
                }
            }
            else
            {
                Console.WriteLine("\n❌ Невірний формат ID.\n");
            }
        }

        static void CreateWorkout()
        {
            Console.WriteLine("\n════════════════════════════════════════════════════════════════");
            Console.WriteLine("                    СТВОРЕННЯ НОВОГО ТРЕНУВАННЯ");
            Console.WriteLine("════════════════════════════════════════════════════════════════\n");

            var trainers = service.GetAllTrainers();
            if (trainers.Count == 0)
            {
                Console.WriteLine("❌ Немає доступних тренерів.\n");
                return;
            }

            Console.WriteLine("Доступні тренери:");
            foreach (var t in trainers)
            {
                Console.WriteLine($"   {t.TrainerId} - {t.FullName} ({t.Specialization})");
            }

            var dto = new CreateWorkoutDto();

            Console.Write("\nID тренера: ");
            if (!Guid.TryParse(Console.ReadLine(), out var trainerId) || service.GetTrainerById(trainerId) == null)
            {
                Console.WriteLine("❌ Невірний ID тренера. Використано першого тренера.");
                dto.TrainerId = trainers[0].TrainerId;
            }
            else
            {
                dto.TrainerId = trainerId;
            }

            Console.Write("Назва тренування: ");
            dto.Title = Console.ReadLine();

            Console.Write("Тип тренування (Yoga, Strength, Cardio, Hiit, Pilates, Crossfit): ");
            dto.WorkoutType = Console.ReadLine();

            Console.Write("Рівень складності (Beginner, Intermediate, Advanced): ");
            dto.Difficulty = Console.ReadLine();

            Console.Write("Тривалість (хвилин): ");
            dto.DurationMinutes = int.TryParse(Console.ReadLine(), out var dur) ? dur : 30;

            Console.Write("Калорій (за бажанням, Enter – пропустити): ");
            var cal = Console.ReadLine();
            dto.CaloriesBurnEstimate = string.IsNullOrWhiteSpace(cal) ? null : int.Parse(cal);

            Console.Write("Опис: ");
            dto.Description = Console.ReadLine();

            try
            {
                var created = service.CreateWorkout(dto);
                Console.WriteLine($"\n✅ Тренування успішно створено!");
                Console.WriteLine($"   ID: {created.WorkoutId}");
                Console.WriteLine($"   Назва: {created.Title}\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n❌ Помилка: {ex.Message}\n");
            }
        }

        static void UpdateWorkout()
        {
            ShowAllWorkouts();
            Console.Write("\n✏️ Введіть ID тренування для оновлення: ");
            if (!Guid.TryParse(Console.ReadLine(), out var id))
            {
                Console.WriteLine("❌ Невірний формат ID.\n");
                return;
            }

            var existing = service.GetWorkoutById(id);
            if (existing == null)
            {
                Console.WriteLine($"❌ Тренування з ID {id} не знайдено.\n");
                return;
            }

            Console.WriteLine("\n════════════════════════════════════════════════════════════════");
            Console.WriteLine($"           ОНОВЛЕННЯ ТРЕНУВАННЯ: {existing.Title}");
            Console.WriteLine("════════════════════════════════════════════════════════════════\n");

            var dto = new UpdateWorkoutDto();

            Console.Write($"Нова назва (поточна: {existing.Title}): ");
            dto.Title = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(dto.Title)) dto.Title = existing.Title;

            Console.Write($"Тип (поточний: {existing.WorkoutType}): ");
            dto.WorkoutType = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(dto.WorkoutType)) dto.WorkoutType = existing.WorkoutType;

            Console.Write($"Складність (поточна: {existing.Difficulty}): ");
            dto.Difficulty = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(dto.Difficulty)) dto.Difficulty = existing.Difficulty;

            Console.Write($"Тривалість (поточна: {existing.DurationMinutes} хв): ");
            var dur = Console.ReadLine();
            dto.DurationMinutes = string.IsNullOrWhiteSpace(dur) ? existing.DurationMinutes : int.Parse(dur);

            Console.Write($"Опис (поточний: {existing.Description}): ");
            dto.Description = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(dto.Description)) dto.Description = existing.Description;

            Console.Write($"Калорій (поточний: {(existing.CaloriesBurnEstimate.HasValue ? existing.CaloriesBurnEstimate.ToString() : "не вказано")}): ");
            var cal = Console.ReadLine();
            dto.CaloriesBurnEstimate = string.IsNullOrWhiteSpace(cal) ? existing.CaloriesBurnEstimate : int.Parse(cal);

            Console.Write($"Активне? (true/false, поточне: {existing.IsActive}): ");
            var active = Console.ReadLine();
            dto.IsActive = string.IsNullOrWhiteSpace(active) ? existing.IsActive : bool.Parse(active);

            if (service.UpdateWorkout(id, dto))
                Console.WriteLine("\n✅ Тренування успішно оновлено!\n");
            else
                Console.WriteLine("\n❌ Помилка при оновленні.\n");
        }

        static void DeleteWorkout()
        {
            ShowAllWorkouts();
            Console.Write("\n🗑️ Введіть ID тренування для видалення: ");
            if (Guid.TryParse(Console.ReadLine(), out var id))
            {
                var workout = service.GetWorkoutById(id);
                if (workout != null)
                {
                    Console.Write($"\nВи впевнені, що хочете видалити \"{workout.Title}\"? (так/ні): ");
                    if (Console.ReadLine()?.ToLower() == "так")
                    {
                        if (service.DeleteWorkout(id))
                            Console.WriteLine($"\n✅ Тренування \"{workout.Title}\" видалено.\n");
                        else
                            Console.WriteLine("\n❌ Помилка при видаленні.\n");
                    }
                    else
                    {
                        Console.WriteLine("\n❌ Видалення скасовано.\n");
                    }
                }
                else
                {
                    Console.WriteLine($"\n❌ Тренування з ID {id} не знайдено.\n");
                }
            }
            else
            {
                Console.WriteLine("\n❌ Невірний формат ID.\n");
            }
        }

        static void FilterByType()
        {
            Console.Write("\n🔍 Введіть тип тренування (Yoga, Strength, Cardio, Hiit, Pilates, Crossfit): ");
            var type = Console.ReadLine();
            var workouts = service.GetWorkoutsByType(type);
            Console.WriteLine($"\n════════════════════════════════════════════════════════════════");
            Console.WriteLine($"              🏋️ ТРЕНУВАННЯ ТИПУ: {type}");
            Console.WriteLine("════════════════════════════════════════════════════════════════\n");

            if (workouts.Count == 0)
            {
                Console.WriteLine($"📭 Немає тренувань типу {type}.\n");
                return;
            }

            foreach (var w in workouts)
            {
                Console.WriteLine($"• {w.Title} - {w.TrainerName} | {w.Difficulty} | {w.DurationMinutes} хв | {(w.IsActive ? "✅" : "❌")}");
            }
            Console.WriteLine();
        }

        static void FilterByDifficulty()
        {
            Console.Write("\n🔍 Введіть рівень складності (Beginner, Intermediate, Advanced): ");
            var diff = Console.ReadLine();
            var workouts = service.GetWorkoutsByDifficulty(diff);
            Console.WriteLine($"\n════════════════════════════════════════════════════════════════");
            Console.WriteLine($"              📊 ТРЕНУВАННЯ РІВНЯ: {diff}");
            Console.WriteLine("════════════════════════════════════════════════════════════════\n");

            if (workouts.Count == 0)
            {
                Console.WriteLine($"📭 Немає тренувань рівня {diff}.\n");
                return;
            }

            foreach (var w in workouts)
            {
                Console.WriteLine($"• {w.Title} - {w.TrainerName} | {w.WorkoutType} | {w.DurationMinutes} хв | {(w.IsActive ? "✅" : "❌")}");
            }
            Console.WriteLine();
        }

        static void FilterByTrainer()
        {
            ShowAllTrainers();
            Console.Write("\n🔍 Введіть ID тренера: ");
            if (Guid.TryParse(Console.ReadLine(), out var trainerId))
            {
                var trainer = service.GetTrainerById(trainerId);
                if (trainer == null)
                {
                    Console.WriteLine($"\n❌ Тренера з ID {trainerId} не знайдено.\n");
                    return;
                }

                var workouts = service.GetWorkoutsByTrainer(trainerId);
                Console.WriteLine($"\n════════════════════════════════════════════════════════════════");
                Console.WriteLine($"              👨‍🏫 ТРЕНУВАННЯ ТРЕНЕРА: {trainer.FullName}");
                Console.WriteLine("════════════════════════════════════════════════════════════════\n");

                if (workouts.Count == 0)
                {
                    Console.WriteLine($"📭 У тренера {trainer.FullName} немає тренувань.\n");
                    return;
                }

                foreach (var w in workouts)
                {
                    Console.WriteLine($"• {w.Title} | {w.WorkoutType} | {w.Difficulty} | {w.DurationMinutes} хв | {(w.IsActive ? "✅" : "❌")}");
                }
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("\n❌ Невірний формат ID.\n");
            }
        }

        static void ShowAllTrainers()
        {
            var trainers = service.GetAllTrainers();
            Console.WriteLine("\n════════════════════════════════════════════════════════════════");
            Console.WriteLine("                      СПИСОК ТРЕНЕРІВ");
            Console.WriteLine("════════════════════════════════════════════════════════════════\n");

            if (trainers.Count == 0)
            {
                Console.WriteLine("📭 Немає зареєстрованих тренерів.\n");
                return;
            }

            foreach (var t in trainers)
            {
                var workoutCount = service.GetWorkoutsByTrainer(t.TrainerId).Count;
                Console.WriteLine($"┌─────────────────────────────────────────────────────────────┐");
                Console.WriteLine($"│ 👨‍🏫 {t.FullName}");
                Console.WriteLine($"├─────────────────────────────────────────────────────────────┤");
                Console.WriteLine($"│ ID:           {t.TrainerId}");
                Console.WriteLine($"│ Спеціалізація: {t.Specialization}");
                Console.WriteLine($"│ Досвід:       {t.ExperienceYears} років");
                Console.WriteLine($"│ Рейтинг:      {t.Rating} ★");
                Console.WriteLine($"│ Тренувань:    {workoutCount}");
                Console.WriteLine($"└─────────────────────────────────────────────────────────────┘\n");
            }
        }

        static void ShowAllClients()
        {
            var clients = service.GetAllClients();
            Console.WriteLine("\n════════════════════════════════════════════════════════════════");
            Console.WriteLine("                      СПИСОК КЛІЄНТІВ");
            Console.WriteLine("════════════════════════════════════════════════════════════════\n");

            if (clients.Count == 0)
            {
                Console.WriteLine("📭 Немає зареєстрованих клієнтів.\n");
                return;
            }

            foreach (var c in clients)
            {
                Console.WriteLine($"┌─────────────────────────────────────────────────────────────┐");
                Console.WriteLine($"│ 👤 {c.FullName}");
                Console.WriteLine($"├─────────────────────────────────────────────────────────────┤");
                Console.WriteLine($"│ ID:          {c.ClientId}");
                Console.WriteLine($"│ Email:       {c.Email}");
                Console.WriteLine($"│ Абонемент:   {c.MembershipType}");
                Console.WriteLine($"│ Дійсний до:  {(c.MembershipExpiresAt.HasValue ? c.MembershipExpiresAt.Value.ToShortDateString() : "не вказано")}");
                Console.WriteLine($"└─────────────────────────────────────────────────────────────┘\n");
            }
        }

        static void ShowStatistics()
        {
            var stats = service.GetStatistics();
            var type = stats.GetType();

            Console.WriteLine("\n╔════════════════════════════════════════════════════════════════╗");
            Console.WriteLine("║                    СТАТИСТИКА FITTRACK                          ║");
            Console.WriteLine("╚════════════════════════════════════════════════════════════════╝\n");

            Console.WriteLine($"📊 Загальна кількість тренувань: {type.GetProperty("TotalWorkouts")?.GetValue(stats)}");
            Console.WriteLine($"✅ Активних тренувань: {type.GetProperty("ActiveWorkouts")?.GetValue(stats)}");
            Console.WriteLine($"👨‍🏫 Кількість тренерів: {type.GetProperty("TotalTrainers")?.GetValue(stats)}");
            Console.WriteLine($"👤 Кількість клієнтів: {type.GetProperty("TotalClients")?.GetValue(stats)}");

            Console.WriteLine("\n📈 Розподіл за типами тренувань:");
            var byType = type.GetProperty("WorkoutsByType")?.GetValue(stats) as IEnumerable<object>;
            if (byType != null)
                foreach (var item in byType)
                {
                    var t = item.GetType();
                    Console.WriteLine($"   {t.GetProperty("Type")?.GetValue(item),-12} : {t.GetProperty("Count")?.GetValue(item)}");
                }

            Console.WriteLine("\n📊 Розподіл за рівнем складності:");
            var byDiff = type.GetProperty("WorkoutsByDifficulty")?.GetValue(stats) as IEnumerable<object>;
            if (byDiff != null)
                foreach (var item in byDiff)
                {
                    var d = item.GetType();
                    Console.WriteLine($"   {d.GetProperty("Difficulty")?.GetValue(item),-12} : {d.GetProperty("Count")?.GetValue(item)}");
                }

            Console.WriteLine($"\n⏱️ Середня тривалість тренування: {type.GetProperty("AverageDuration")?.GetValue(stats):F0} хвилин");

            var avgCal = type.GetProperty("AverageCalories")?.GetValue(stats);
            if (avgCal != null)
                Console.WriteLine($"🔥 Середнє спалювання калорій: {avgCal:F0} ккал");

            Console.WriteLine();
        }
    }
}