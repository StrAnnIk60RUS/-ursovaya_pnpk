
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ConsoleApp3
{
    public class Program
    {

        public struct Student
        {
            public int StudentId;
            public string LastName;
            public string FirstName;
            public string Patronymic;
            public DateTime DateOfBirth;
            public string Gender;
            public string Faculty;
            public string Specialty;
            public int Course;
            public string Group;
            public int NumberOfExams;
            public int[] ExamGrades;

            public Student(int studentId, string lastName, string firstName, string patronymic, DateTime dateOfBirth, string gender,
                string faculty, string specialty, int course, string group, int numberOfExams, int[] examGrades)
            {
                StudentId = studentId;
                LastName = lastName;
                FirstName = firstName;
                Patronymic = patronymic;
                DateOfBirth = dateOfBirth;
                Gender = gender;
                Faculty = faculty;
                Specialty = specialty;
                Course = course;
                Group = group;
                NumberOfExams = numberOfExams;
                ExamGrades = examGrades;
            }
        }

        public struct Specialty
        {
            public int SpecialtyId;
            public string SpecialtyName;

            public Specialty(int specialtyId, string specialtyName)
            {
                SpecialtyId = specialtyId;
                SpecialtyName = specialtyName;
            }
        }

        public struct Faculty
        {
            public int FacultyId;
            public string FacultyName;

            public Faculty(int facultyId, string facultyName)
            {
                FacultyId = facultyId;
                FacultyName = facultyName;
            }
        }

        public static void Main(string[] args)
        {
            string facultiesPath = @"data\Faculties.txt";
            string specialtiesPath = @"data\Specialties.txt";
            string studentsPath = @"data\Students.txt";

            CheckExistenceOfFiles(studentsPath, facultiesPath, specialtiesPath);

            List<Student> allStudents = ReadStudentsFromFile(studentsPath);
            List<Faculty> allFaculties = ReadFacultiesFromFile(facultiesPath);
            List<Specialty> allSpecialties = ReadSpecialtiesFromFile(specialtiesPath);

            MainMenu(allStudents, allFaculties, allSpecialties);
        }

        public static void MainMenu(List<Student> allStudents, List<Faculty> allFaculties, List<Specialty> allSpecialties)
        {
            while (true)
            {
                Console.WriteLine("\n=== Информационная система ВУЗа ===");
                Console.WriteLine("1) Управление данными студентов");
                Console.WriteLine("2) Управление справочниками");
                Console.WriteLine("3) Сортировка данных студентов");
                Console.WriteLine("4) Генерация отчетов");
                Console.WriteLine("0) Выход");

                Console.Write("Введите номер действия: ");
                string numOfMainActions = Console.ReadLine();
                switch (numOfMainActions)
                {
                    case "1":
                        Console.Clear();
                        ManageStudentData(allStudents, allFaculties, allSpecialties);
                        break;
                    case "2":
                        Console.Clear();
                        ManageDirectories(allFaculties, allSpecialties);
                        Console.Clear();
                        break;
                    case "3":
                        Console.Clear();
                        SortStudentData(allStudents);
                        break;
                    case "4":
                        Console.Clear();
                        GenerateReports(allStudents, allFaculties, allSpecialties);
                        break;
                    case "0":
                        Console.Clear();
                        ExitMenu(allStudents, allFaculties, allSpecialties);
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("Вы ввели что-то не так. Попробуйте снова.");
                        break;
                }
            }
        }

        public static void ExitMenu(List<Student> allStudents, List<Faculty> allFaculties, List<Specialty> allSpecialties)
        {
            bool flagExitMenu = false;
            while (!flagExitMenu)
            {
                Console.WriteLine("\nВыберите действие:");
                Console.WriteLine("1) Сохранить данные");
                Console.WriteLine("2) Не сохранять данные");
                Console.WriteLine("0) Назад");

                Console.Write("Введите номер действия: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        SaveStudentsToFile(allStudents, @"data\Students.txt");
                        SaveFacultiesToFile(allFaculties, @"data\Faculties.txt");
                        SaveSpecialtiesToFile(allSpecialties, @"data\Specialties.txt");
                        Console.WriteLine("Данные успешно сохранены.");
                        Environment.Exit(0);
                        break;

                    case "2":
                        Console.WriteLine("Данные не сохранены.");
                        Environment.Exit(0);
                        break;
                    case "0":
                        flagExitMenu = true;
                        Console.Clear();
                        break;

                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }
            }
        }


        public static void ManageStudentData(List<Student> allStudents, List<Faculty> allFaculties, List<Specialty> allSpecialties)
        {
            while (true)
            {
                Console.WriteLine("1) Добавить студента");
                Console.WriteLine("2) Изменить данные студента");
                Console.WriteLine("3) Удалить студента");
                Console.WriteLine("4) Поиск студентов");
                Console.WriteLine("5) Вывод всех студентов");
                Console.WriteLine("0) Назад");
                Console.Write("Выберите действие: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        AddStudent(allStudents, allFaculties, allSpecialties);
                        Console.Clear();
                        break;
                    case "2":
                        Console.Clear();
                        int RedactingStudentIndex = SearchStudentIndexById(allStudents, GetStudentId(allStudents));
                        if (RedactingStudentIndex == -3 || RedactingStudentIndex >= allStudents.Count)
                        {
                            Console.WriteLine("Студент не найден.");
                            Console.WriteLine("Нажмите Enter...");
                            Console.ReadLine();
                        }
                        if (RedactingStudentIndex != -2 && RedactingStudentIndex != -1)
                        {
                            EditStudentRecord(RedactingStudentIndex, allStudents, allFaculties, allSpecialties);
                            Console.WriteLine("Нажмите Enter...");
                            Console.ReadLine();
                        }
                        Console.Clear();
                        break;
                    case "3":
                        int DeletingStudentIndex = SearchStudentIndexById(allStudents, GetStudentId(allStudents)); ;
                        if (DeletingStudentIndex == -3 || DeletingStudentIndex >= allStudents.Count)
                        {
                            Console.WriteLine("Студент не найден.");
                            Console.WriteLine("Нажмите Enter...");
                            Console.ReadLine();
                        }
                        if (DeletingStudentIndex != -2 && DeletingStudentIndex != -1)
                        {
                            RemoveStudentByIndex(allStudents, DeletingStudentIndex);
                            Console.WriteLine("Нажмите Enter...");
                            Console.ReadLine();
                        }
                        Console.Clear();
                        break;
                    case "4":
                        Console.Clear();
                        foreach (Student student in SearchStudentMenu(allStudents, allFaculties, allSpecialties))
                        {
                            DisplayStudent(student);
                        }
                        Console.WriteLine("Нажмите Enter...");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case "5":
                        Console.Clear();
                        foreach (var student in allStudents)
                        {
                            DisplayStudent(student);
                        }
                        Console.WriteLine("Нажмите Enter...");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case "0":
                        Console.Clear();
                        return;
                    default:
                        Console.Clear();
                        Console.WriteLine("Неверный выбор. Нажмите Enter, чтобы продолжить.");
                        Console.ReadLine();
                        break;
                }
            }
        }

        public static List<Student> SearchStudentMenu(List<Student> allStudents, List<Faculty> allFaculties, List<Specialty> allSpecialties)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Меню поиска студентов");
                Console.WriteLine("1) Поиск по ФИО");
                Console.WriteLine("2) Поиск по дате рождения");
                Console.WriteLine("3) Поиск по факультету");
                Console.WriteLine("4) Поиск по специальности");
                Console.WriteLine("5) Поиск по курсу");
                Console.WriteLine("6) Поиск по группе");
                Console.WriteLine("0) Назад");
                Console.Write("Выберите действие: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        string firstName = GetInputSring("Введите фамилию: ", false);
                        if (firstName == null) return new List<Student> ();
                        string lastName = GetInputSring("Введите имя: ", false);
                        if (lastName == null) return new List<Student>();
                        string patronymic = GetInputSring("Введите отчество: ", false);
                        if (patronymic == null) return new List<Student>();

                        return SearchByFullName(allStudents, firstName, lastName, patronymic);
                    case "2":
                        DateTime startDate = GetDateInput("Введите начальную дату ", false);
                        DateTime endDate = GetDateInput("Введите конечную дату ", false);
                        return SearchByDateOfBirth(allStudents, startDate, endDate);
                    case "3":
                        string selectedFaculty = SelectFaculty(allFaculties, null, false);
                        return FindStudentsByFaculty(allStudents, selectedFaculty);
                    case "4":
                        string selectedSpeciality = SelectSpecialty(allSpecialties, null, false);
                        return FindStudentsBySpecialty(allStudents, selectedSpeciality);
                    case "5":
                        int course = GetInputCourse(false);
                        return FindStudentsByCourse(allStudents, course);
                    case "6":
                        string group = GetInputSring("Напишите группу: ", false);
                        return FindStudentsByGroup(allStudents, group);
                    case "0":
                        Console.Clear();
                        return new List<Student>();
                    default:
                        Console.WriteLine("Неверный выбор. Нажмите Enter, чтобы продолжить.");
                        Console.ReadLine();
                        break;
                }
            }
        }

        public static void ManageDirectories(List<Faculty> allFaculties, List<Specialty> allSpecialties)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1) Факультеты");
                Console.WriteLine("2) Специальности");
                Console.WriteLine("0) Назад");
                Console.Write("Выберите действие: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        ManageFaculties(allFaculties);
                        break;
                    case "2":
                        ManageSpecialties(allSpecialties);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Нажмите Enter, чтобы продолжить.");
                        Console.ReadLine();
                        break;
                }
            }
        }

        public static void ManageFaculties(List<Faculty> allFaculties)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1) Просмотр факультетов");
                Console.WriteLine("2) Добавить факультет");
                Console.WriteLine("3) Изменить факультет");
                Console.WriteLine("4) Удалить факультет");
                Console.WriteLine("0) Назад");
                Console.Write("Выберите действие: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Просмотр факультетов...");
                        ViewFaculties(allFaculties);
                        break;
                    case "2":
                        Console.WriteLine("Добавление факультета...");
                        AddFaculty(allFaculties);
                        break;
                    case "3":
                        Console.WriteLine("Изменение факультета...");
                        EditFaculty(allFaculties);
                        break;
                    case "4":
                        Console.WriteLine("Удаление факультета...");
                        DeleteFaculty(allFaculties);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Нажмите Enter, чтобы продолжить.");
                        Console.ReadLine();
                        break;
                }
            }
        }

        public static void ManageSpecialties(List<Specialty> allSpecialties)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1) Просмотр специальностей");
                Console.WriteLine("2) Добавить специальность");
                Console.WriteLine("3) Изменить специальность");
                Console.WriteLine("4) Удалить специальность");
                Console.WriteLine("0) Назад");
                Console.Write("Выберите действие: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Просмотр специальностей...");
                        ViewSpecialties(allSpecialties);
                        break;
                    case "2":
                        Console.WriteLine("Добавление специальности...");
                        AddSpecialty(allSpecialties);
                        break;
                    case "3":
                        Console.WriteLine("Изменение специальности...");
                        EditSpecialty(allSpecialties);
                        break;
                    case "4":
                        Console.WriteLine("Удаление специальности...");
                        DeleteSpecialty(allSpecialties);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Нажмите Enter, чтобы продолжить.");
                        Console.ReadLine();
                        break;
                }
            }
        }

        public static void ViewFaculties(List<Faculty> allFaculties)
        {
            Console.Clear();
            Console.WriteLine("Просмотр факультетов...");
            if (allFaculties.Count == 0)
            {
                Console.WriteLine("Нет факультетов для отображения.");
            }
            else
            {
                for (int i = 0; i < allFaculties.Count; i++)
                {
                    Console.WriteLine($"{allFaculties[i].FacultyId}. {allFaculties[i].FacultyName}");
                }
            }

            Console.WriteLine("\nНажмите Enter...");
            Console.ReadLine();

        }
        public static void AddFaculty(List<Faculty> allFaculties)
        {
            Console.Clear();
            Console.WriteLine("Добавление факультета...");
            Console.Write("Введите название факультета (или 'выход' для отмены): ");
            string facultyName = Console.ReadLine();

            if (facultyName.ToLower() == "выход")
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(facultyName))
            {
                Console.WriteLine("Название факультета не может быть пустым.");
            }
            else
            {
                int newId = 1;
                if (allFaculties.Count > 0)
                {
                    int maxId = 0;
                    foreach (var faculty in allFaculties)
                    {
                        if (faculty.FacultyId > maxId)
                        {
                            maxId = faculty.FacultyId;
                        }
                    }
                    newId = maxId + 1;
                }

                allFaculties.Add(new Faculty(newId, facultyName));
                Console.WriteLine($"Факультет '{facultyName}' успешно добавлен.");
            }

            Console.WriteLine("\nНажмите Enter для возвращения в меню.");
            Console.ReadLine();
        }
        public static void EditFaculty(List<Faculty> allFaculties)
        {
            Console.Clear();
            Console.WriteLine("Изменение факультета...");
            if (allFaculties.Count == 0)
            {
                Console.WriteLine("Нет факультетов для редактирования.");
            }
            else
            {
                for (int i = 0; i < allFaculties.Count; i++)
                {
                    Console.WriteLine($"{allFaculties[i].FacultyId}. {allFaculties[i].FacultyName}");
                }

                Console.Write("Выберите номер факультета для редактирования (или 'выход' для отмены): ");
                string input = Console.ReadLine();

                if (input.ToLower() == "выход")
                {
                    return;
                }

                if (int.TryParse(input, out int choice) && choice > 0 && choice <= allFaculties.Count)
                {
                    Console.Write("Введите новое название факультета: ");
                    string newName = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(newName))
                    {
                        Console.WriteLine("Название факультета не может быть пустым.");
                    }
                    else
                    {
                        Faculty updatedFaculty = new Faculty(allFaculties[choice - 1].FacultyId, newName);
                        allFaculties[choice - 1] = updatedFaculty;
                        Console.WriteLine($"Факультет успешно изменен на '{newName}'.");
                    }
                }
                else
                {
                    Console.WriteLine("Некорректный ввод.");
                }
            }

            Console.WriteLine("\nНажмите Enter для возвращения в меню.");
            Console.ReadLine();
        }
        public static void DeleteFaculty(List<Faculty> allFaculties)
        {
            Console.Clear();
            Console.WriteLine("Удаление факультета...");
            if (allFaculties.Count == 0)
            {
                Console.WriteLine("Нет факультетов для удаления.");
            }
            else
            {
                for (int i = 0; i < allFaculties.Count; i++)
                {
                    Console.WriteLine($"{allFaculties[i].FacultyId}. {allFaculties[i].FacultyName}");
                }

                Console.Write("Введите номер факультета для удаления (или 'выход' для отмены): ");
                string input = Console.ReadLine();

                if (input.ToLower() == "выход")
                {
                    return;
                }

                if (int.TryParse(input, out int idToDelete))
                {
                    int indexToRemove = -1;
                    for (int i = 0; i < allFaculties.Count; i++)
                    {
                        if (allFaculties[i].FacultyId == idToDelete)
                        {
                            indexToRemove = i;
                            break;
                        }
                    }

                    if (indexToRemove != -1)
                    {
                        allFaculties.RemoveAt(indexToRemove);
                        for (int i = 0; i < allFaculties.Count; i++)
                        {
                            allFaculties[i] = new Faculty(i + 1, allFaculties[i].FacultyName);
                        }

                        Console.WriteLine($"Факультет с номером {idToDelete} успешно удален.");
                    }
                    else
                    {
                        Console.WriteLine($"Факультет с номером {idToDelete} не найден.");
                    }
                }
                else
                {
                    Console.WriteLine("Некорректный ввод.");
                }
            }

            Console.WriteLine("\nНажмите Enter для возвращения в меню.");
            Console.ReadLine();
        }

        public static void ViewSpecialties(List<Specialty> allSpecialties)
        {
            Console.Clear();
            Console.WriteLine("Просмотр специальностей...");
            if (allSpecialties.Count == 0)
            {
                Console.WriteLine("Нет специальностей для отображения.");
            }
            else
            {
                for (int i = 0; i < allSpecialties.Count; i++)
                {
                    Console.WriteLine($"{allSpecialties[i].SpecialtyId}. {allSpecialties[i].SpecialtyName}");
                }
            }

            Console.WriteLine("\nНажмите Enter...");
            Console.ReadLine();
        }
        public static void AddSpecialty(List<Specialty> allSpecialties)
        {
            Console.Clear();
            Console.WriteLine("Добавление специальности...");
            Console.Write("Введите название специальности (или 'выход' для отмены): ");
            string specialtyName = Console.ReadLine();

            if (specialtyName.ToLower() == "выход")
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(specialtyName))
            {
                Console.WriteLine("Название специальности не может быть пустым.");
            }
            else
            {
                int newId = 1;
                if (allSpecialties.Count > 0)
                {
                    int maxId = 0;
                    foreach (var specialty in allSpecialties)
                    {
                        if (specialty.SpecialtyId > maxId)
                        {
                            maxId = specialty.SpecialtyId;
                        }
                    }
                    newId = maxId + 1;
                }

                allSpecialties.Add(new Specialty(newId, specialtyName));
                Console.WriteLine($"Специальность '{specialtyName}' успешно добавлена.");
            }

            Console.WriteLine("\nНажмите Enter для возвращения в меню.");
            Console.ReadLine();
        }
        public static void EditSpecialty(List<Specialty> allSpecialties)
        {
            Console.Clear();
            Console.WriteLine("Изменение специальности...");
            if (allSpecialties.Count == 0)
            {
                Console.WriteLine("Нет специальностей для редактирования.");
            }
            else
            {
                for (int i = 0; i < allSpecialties.Count; i++)
                {
                    Console.WriteLine($"{allSpecialties[i].SpecialtyId}. {allSpecialties[i].SpecialtyName}");
                }

                Console.Write("Выберите номер специальности для редактирования (или 'выход' для отмены): ");
                string input = Console.ReadLine();

                if (input.ToLower() == "выход")
                {
                    return;
                }

                if (int.TryParse(input, out int choice) && choice > 0 && choice <= allSpecialties.Count)
                {
                    Console.Write("Введите новое название специальности: ");
                    string newName = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(newName))
                    {
                        Console.WriteLine("Название специальности не может быть пустым.");
                    }
                    else
                    {
                        Specialty updatedSpecialty = new Specialty(allSpecialties[choice - 1].SpecialtyId, newName);
                        allSpecialties[choice - 1] = updatedSpecialty;
                        Console.WriteLine($"Специальность успешно изменена на '{newName}'.");
                    }
                }
                else
                {
                    Console.WriteLine("Некорректный ввод.");
                }
            }

            Console.WriteLine("\nНажмите Enter для возвращения в меню.");
            Console.ReadLine();
        }
        public static void DeleteSpecialty(List<Specialty> allSpecialties)
        {
            Console.Clear();
            Console.WriteLine("Удаление специальности...");
            if (allSpecialties.Count == 0)
            {
                Console.WriteLine("Нет специальностей для удаления.");
            }
            else
            {
                for (int i = 0; i < allSpecialties.Count; i++)
                {
                    Console.WriteLine($"{allSpecialties[i].SpecialtyId}. {allSpecialties[i].SpecialtyName}");
                }

                Console.Write("Введите номер специальности для удаления (или 'выход' для отмены): ");
                string input = Console.ReadLine();

                if (input.ToLower() == "выход")
                {
                    return;
                }

                if (int.TryParse(input, out int idToDelete))
                {
                    int indexToRemove = -1;
                    for (int i = 0; i < allSpecialties.Count; i++)
                    {
                        if (allSpecialties[i].SpecialtyId == idToDelete)
                        {
                            indexToRemove = i;
                            break;
                        }
                    }

                    if (indexToRemove != -1)
                    {
                        allSpecialties.RemoveAt(indexToRemove);
                        for (int i = 0; i < allSpecialties.Count; i++)
                        {
                            allSpecialties[i] = new Specialty(i + 1, allSpecialties[i].SpecialtyName);
                        }

                        Console.WriteLine($"Специальность с номером {idToDelete} успешно удалена.");
                    }
                    else
                    {
                        Console.WriteLine($"Специальность с номером {idToDelete} не найдена.");
                    }
                }
                else
                {
                    Console.WriteLine("Некорректный ввод.");
                }
            }

            Console.WriteLine("\nНажмите Enter для возвращения в меню.");
            Console.ReadLine();
        }


        public static void SortStudentData(List<Student> allStudents)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("1) По номеру студенческого билета");
                Console.WriteLine("2) По ФИО");
                Console.WriteLine("3) По факультету");
                Console.WriteLine("4) По специальности");
                Console.WriteLine("5) По курсу");
                Console.WriteLine("6) По группе");
                Console.WriteLine("0) Назад");
                Console.Write("Выберите действие: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Console.WriteLine("Сортировка по номеру студенческого билета...");
                        allStudents.Sort((a, b) => a.StudentId.CompareTo(b.StudentId));
                        break;
                    case "2":
                        Console.WriteLine("Сортировка по ФИО...");
                        allStudents.Sort((a, b) => CompareByFullName(a, b));
                        break;
                    case "3":
                        Console.WriteLine("Сортировка по факультету...");
                        allStudents.Sort((a, b) =>
                            string.Compare(a.Faculty, b.Faculty, StringComparison.Ordinal) != 0 ?
                            string.Compare(a.Faculty, b.Faculty, StringComparison.Ordinal) :
                            CompareByFullName(a, b));
                        break;
                    case "4":
                        Console.WriteLine("Сортировка по специальности...");
                        allStudents.Sort((a, b) =>
                            string.Compare(a.Specialty, b.Specialty, StringComparison.Ordinal) != 0 ?
                            string.Compare(a.Specialty, b.Specialty, StringComparison.Ordinal) :
                            CompareByFullName(a, b));
                        break;
                    case "5":
                        Console.WriteLine("Сортировка по курсу...");
                        allStudents.Sort((a, b) =>
                            a.Course.CompareTo(b.Course) != 0 ?
                            a.Course.CompareTo(b.Course) :
                            CompareByFullName(a, b));
                        break;
                    case "6":
                        Console.WriteLine("Сортировка по группе...");
                        allStudents.Sort((a, b) =>
                            string.Compare(a.Group, b.Group, StringComparison.Ordinal) != 0 ?
                            string.Compare(a.Group, b.Group, StringComparison.Ordinal) :
                            CompareByFullName(a, b));
                        break;
                    case "0":
                        Console.Clear();
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Нажмите Enter, чтобы продолжить.");
                        Console.ReadLine();
                        break;
                }

                Console.WriteLine("Отсортированные студенты:");
                foreach (var student in allStudents)
                {
                    Console.WriteLine($"{student.StudentId}: {student.LastName} {student.FirstName} {student.Patronymic}, {student.Faculty}, {student.Specialty}, {student.Course} курс, группа {student.Group}");
                }
                Console.WriteLine("Нажмите Enter для продолжения...");
                Console.ReadLine();
            }
        }

        public static int CompareByFullName(Student a, Student b)
        {
            int comparison = string.Compare(a.LastName, b.LastName, StringComparison.Ordinal);
            if (comparison != 0) return comparison;

            comparison = string.Compare(a.FirstName, b.FirstName, StringComparison.Ordinal);
            if (comparison != 0) return comparison;

            return string.Compare(a.Patronymic, b.Patronymic, StringComparison.Ordinal);
        }

        public static void GenerateReports(List<Student> allStudents, List<Faculty> allFaculties, List<Specialty> allSpecialties)
        {
            while (true)
            {
                Console.WriteLine("1) Список студентов с двойками");
                Console.WriteLine("2) Список студентов на стипендию");
                Console.WriteLine("3) Анализ успеваемости по группам и факультетам");
                Console.WriteLine("0) Назад");
                Console.Write("Выберите действие: ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        GenerateReportByFacultyAndSpecialty(allFaculties, allStudents, @"data\WithDoubles.txt");
                        break;
                    case "2":
                        GenerateScholarshipReport(allFaculties, allStudents, @"data\WithSalary.txt");
                        break;
                    case "3":
                        GenerateGroupPerformanceReport(allFaculties, allStudents, @"data\PerfomanceReport.txt");
                        break;
                    case "0":
                        Console.Clear();
                        return;
                    default:
                        Console.WriteLine("Неверный выбор. Нажмите Enter, чтобы продолжить.");
                        Console.ReadLine();
                        break;
                }
            }
        }

        public static List<Student> SearchByDateOfBirth(List<Student> allStudents, DateTime startDate, DateTime endDate)
        {
            List<Student> results = new List<Student>();
            foreach (var student in allStudents)
            {
                if (student.DateOfBirth >= startDate && student.DateOfBirth <= endDate)
                {
                    results.Add(student);
                }
            }
            return results;
        }
        public static List<Student> FindStudentsByFaculty(List<Student> allStudents, string selectedFaculty)
        {
            if (string.IsNullOrEmpty(selectedFaculty))
            {
                Console.WriteLine("Факультет не выбран.");
                return new List<Student>();
            }

            List<Student> filteredStudents = new List<Student>();

            foreach (var student in allStudents)
            {
                if (student.Faculty == selectedFaculty)
                {
                    filteredStudents.Add(student);
                }
            }

            if (filteredStudents.Count == 0)
            {
                Console.WriteLine($"Не найдено студентов на факультете: {selectedFaculty}");
            }

            return filteredStudents;
        }
        public static List<Student> FindStudentsBySpecialty(List<Student> allStudents, string selectedSpecialty)
        {
            if (string.IsNullOrEmpty(selectedSpecialty))
            {
                Console.WriteLine("Специальность не выбрана.");
                return new List<Student>();
            }

            List<Student> filteredStudents = new List<Student>();

            foreach (var student in allStudents)
            {
                if (student.Specialty == selectedSpecialty)
                {
                    filteredStudents.Add(student);
                }
            }

            if (filteredStudents.Count == 0)
            {
                Console.WriteLine($"Не найдено студентов по специальности: {selectedSpecialty}");
            }

            return filteredStudents;
        }
        public static List<Student> FindStudentsByCourse(List<Student> allStudents, int course)
        {
            List<Student> filteredStudents = new List<Student>();

            foreach (var student in allStudents)
            {
                if (student.Course == course)
                {
                    filteredStudents.Add(student);
                }
            }

            if (filteredStudents.Count == 0)
            {
                Console.WriteLine($"Не найдено студентов на курсе: {course}");
            }

            return filteredStudents;
        }
        public static List<Student> FindStudentsByGroup(List<Student> allStudents, string group)
        {
            List<Student> filteredStudents = new List<Student>();

            foreach (var student in allStudents)
            {
                if (student.Group.Equals(group, StringComparison.OrdinalIgnoreCase))
                {
                    filteredStudents.Add(student);
                }
            }

            if (filteredStudents.Count == 0)
            {
                Console.WriteLine($"Не найдено студентов в группе: {group}");
            }

            return filteredStudents;
        }


        public static void GenerateReportByFacultyAndSpecialty(List<Faculty> faculties, List<Student> students, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var faculty in faculties)
                {
                    writer.WriteLine($"Факультет: {faculty.FacultyName}");
                    Console.WriteLine($"Факультет: {faculty.FacultyName}");

                    List<Student> studentsWithDoubts = new List<Student>();

                    foreach (var student in students)
                    {
                        if (student.Faculty == faculty.FacultyName)
                        {
                            int countOfDoubts = CountDoubts(student.ExamGrades);

                            if (countOfDoubts > 0)
                            {
                                studentsWithDoubts.Add(student);
                            }
                        }
                    }

                    studentsWithDoubts.Sort((x, y) =>
                    {
                        int result = x.Course.CompareTo(y.Course);
                        if (result == 0)
                        {
                            result = string.Compare(x.Group, y.Group);
                            if (result == 0)
                            {
                                result = string.Compare(x.LastName, y.LastName);
                            }
                        }
                        return result;
                    });

                    string header = string.Format("{0,-4} | {1,-6} | {2,-8} | {3,-30} | {4,-15}", "№ п/п", "Курс", "Группа", "ФИО", "Количество двоек");
                    writer.WriteLine(header);
                    Console.WriteLine(header);

                    string border = new string('-', header.Length);
                    writer.WriteLine(border);
                    Console.WriteLine(border);

                    int index = 1;
                    foreach (var student in studentsWithDoubts)
                    {
                        string studentInfo = string.Format("{0,-4} | {1,-6} | {2,-8} | {3,-30} | {4,-15}",
                            index,
                            student.Course,
                            student.Group,
                            $"{student.LastName} {student.FirstName} {student.Patronymic}",
                            CountDoubts(student.ExamGrades)
                        );
                        writer.WriteLine(studentInfo);
                        Console.WriteLine(studentInfo);
                        index++;
                    }
                }
            }
        }
        public static void GenerateScholarshipReport(List<Faculty> allFaculties, List<Student> allStudents, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var faculty in allFaculties)
                {
                    writer.WriteLine($"Факультет: {faculty.FacultyName}");
                    Console.WriteLine($"Факультет: {faculty.FacultyName}");

                    List<Student> studentsForScholarship = new List<Student>();
                    List<Student> studentsForIncreasedScholarship = new List<Student>();

                    foreach (var student in allStudents)
                    {
                        if (student.Faculty == faculty.FacultyName)
                        {
                            if (HasNoGradesLessThan4(student))
                            {
                                studentsForScholarship.Add(student);
                            }

                            if (HasAllGradesEqualTo5(student))
                            {
                                studentsForIncreasedScholarship.Add(student);
                            }
                        }
                    }

                    studentsForScholarship.Sort((x, y) =>
                    {
                        int result = x.Course.CompareTo(y.Course);
                        if (result == 0)
                        {
                            result = string.Compare(x.Group, y.Group);
                            if (result == 0)
                            {
                                result = string.Compare(x.LastName, y.LastName);
                            }
                        }
                        return result;
                    });

                    studentsForIncreasedScholarship.Sort((x, y) =>
                    {
                        int result = x.Course.CompareTo(y.Course);
                        if (result == 0)
                        {
                            result = string.Compare(x.Group, y.Group);
                            if (result == 0)
                            {
                                result = string.Compare(x.LastName, y.LastName);
                            }
                        }
                        return result;
                    });

                    writer.WriteLine("\nПредставленные на стипендию:");
                    Console.WriteLine("\nПредставленные на стипендию:");

                    string header = string.Format("{0,-4} | {1,-6} | {2,-8} | {3,-30}", "№ п/п", "Курс", "Группа", "ФИО");
                    writer.WriteLine(header);
                    Console.WriteLine(header);

                    string border = new string('-', header.Length);
                    writer.WriteLine(border);
                    Console.WriteLine(border);

                    int index = 1;
                    foreach (var student in studentsForScholarship)
                    {
                        string studentInfo = string.Format("{0,-4} | {1,-6} | {2,-8} | {3,-30}",
                            index,
                            student.Course,
                            student.Group,
                            $"{student.LastName} {student.FirstName} {student.Patronymic}"
                        );
                        writer.WriteLine(studentInfo);
                        Console.WriteLine(studentInfo);
                        index++;
                    }

                    writer.WriteLine("\nПредставленные на повышенную стипендию:");
                    Console.WriteLine("\nПредставленные на повышенную стипендию:");

                    writer.WriteLine(header);
                    Console.WriteLine(header);

                    writer.WriteLine(border);
                    Console.WriteLine(border);

                    index = 1;
                    foreach (var student in studentsForIncreasedScholarship)
                    {
                        string studentInfo = string.Format("{0,-4} | {1,-6} | {2,-8} | {3,-30}",
                            index,
                            student.Course,
                            student.Group,
                            $"{student.LastName} {student.FirstName} {student.Patronymic}"
                        );
                        writer.WriteLine(studentInfo);
                        Console.WriteLine(studentInfo);
                        index++;
                    }
                }
            }
        }
        public static bool HasNoGradesLessThan4(Student student)
        {
            foreach (var grade in student.ExamGrades)
            {
                if (grade < 4)
                {
                    return false;
                }
            }
            return true;
        }
        public static bool HasAllGradesEqualTo5(Student student)
        {
            foreach (var grade in student.ExamGrades)
            {
                if (grade != 5)
                {
                    return false;
                }
            }
            return true;
        }
        public static int CountDoubts(int[] ExamGrades)
        {
            int count = 0;
            foreach (var grade in ExamGrades)
            {
                if (grade == 2)
                {
                    count++;
                }
            }
            return count;
        }
        public static void GenerateGroupPerformanceReport(List<Faculty> allFaculties, List<Student> allStudents, string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                int totalStudentsInUniversity = 0;
                int totalFailedStudentsInUniversity = 0;

                foreach (var faculty in allFaculties)
                {
                    writer.WriteLine($"Факультет: {faculty.FacultyName}");
                    Console.WriteLine($"Факультет: {faculty.FacultyName}");

                    string header = string.Format("{0,-6} | {1,-6} | {2,-8} | {3,-18} | {4,-18} | {5,-20}",
                        "№ п/п", "Курс", "Группа", "Кол-во студентов", "Кол-во неуспев.", "Процент успеваемости");
                    writer.WriteLine(header);
                    Console.WriteLine(header);

                    int facultyTotalStudents = 0;
                    int facultyTotalFailedStudents = 0;
                    int counter = 1;

                    var groupsByCourse = new Dictionary<(int, string), List<Student>>();

                    foreach (var student in allStudents)
                    {
                        if (student.Faculty == faculty.FacultyName)
                        {
                            var key = (student.Course, student.Group);
                            if (!groupsByCourse.ContainsKey(key))
                            {
                                groupsByCourse[key] = new List<Student>();
                            }
                            groupsByCourse[key].Add(student);
                        }
                    }

                    foreach (var group in groupsByCourse)
                    {
                        int groupStudentsCount = group.Value.Count;
                        int groupFailedStudents = 0;

                        foreach (var student in group.Value)
                        {
                            foreach (var grade in student.ExamGrades)
                            {
                                if (grade < 4)
                                {
                                    groupFailedStudents++;
                                    break;
                                }
                            }
                        }

                        facultyTotalStudents += groupStudentsCount;
                        facultyTotalFailedStudents += groupFailedStudents;

                        totalStudentsInUniversity += groupStudentsCount;
                        totalFailedStudentsInUniversity += groupFailedStudents;

                        int successPercentage = groupStudentsCount > 0
                            ? (groupStudentsCount - groupFailedStudents) * 100 / groupStudentsCount
                            : 0;

                        string line = string.Format("{0,-6} | {1,-6} | {2,-8} | {3,-18} | {4,-18} | {5,-20}",
                            counter++, group.Key.Item1, group.Key.Item2, groupStudentsCount, groupFailedStudents, successPercentage);
                        writer.WriteLine(line);
                        Console.WriteLine(line);
                    }

                    int facultySuccessPercentage = facultyTotalStudents > 0
                        ? (facultyTotalStudents - facultyTotalFailedStudents) * 100 / facultyTotalStudents
                        : 0;

                    string facultyTotalLine = string.Format("{0,-6} | {1,-6} | {2,-8} | {3,-18} | {4,-18} | {5,-20}",
                        "Итого:", "Все", "Все", facultyTotalStudents, facultyTotalFailedStudents, facultySuccessPercentage);
                    writer.WriteLine(facultyTotalLine);
                    Console.WriteLine(facultyTotalLine);
                }

                int universitySuccessPercentage = totalStudentsInUniversity > 0
                    ? (totalStudentsInUniversity - totalFailedStudentsInUniversity) * 100 / totalStudentsInUniversity
                    : 0;

                string universityTotalLine = string.Format("{0,-6} | {1,-6} | {2,-8} | {3,-18} | {4,-18} | {5,-20}",
                    "Итого по ВУЗу:", "Все", "Все", totalStudentsInUniversity, totalFailedStudentsInUniversity, universitySuccessPercentage);
                writer.WriteLine(universityTotalLine);
                Console.WriteLine(universityTotalLine);
            }
        }


        public static void RemoveStudentByIndex(List<Student> allStudents, int StudentIndex)
        {
            if (StudentIndex >= 0 && StudentIndex < allStudents.Count)
            {
                allStudents.RemoveAt(StudentIndex);
                Console.WriteLine("Студент удален.");
            }
            else
            {
                Console.WriteLine("Ошибка: индекс за пределами списка.");
            }
        }
        public static int SearchStudentIndexById(List<Student> allStudents, int studentId)
        {
            if (studentId == -1)
            {
                return studentId;
            }

            for (int i = 0; i < allStudents.Count; i++)
            {
                if (allStudents[i].StudentId == studentId)
                {
                    return i;
                }
            }


            return -3;
        }
        public static List<Student> SearchByFullName(List<Student> allStudents, string lastName, string firstName, string patronymic)
        {
            List<Student> results = new List<Student>();
            foreach (var student in allStudents)
            {
                if (student.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase) &&
                    student.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase) &&
                    student.Patronymic.Equals(patronymic, StringComparison.OrdinalIgnoreCase))
                {
                    results.Add(student);
                }
            }
            return results;
        }
        public static int GetUniqueStudentId(List<Student> allStudents)
        {
            string studentIdInput;
            int studentId;

            do
            {
                Console.Write("Введите номер студенческого билета (8 цифр): ");
                studentIdInput = Console.ReadLine();

                if (studentIdInput.ToLower() == "выход") return -1;
            }
            while (!IsValidStudentId(studentIdInput, out studentId) || !IsStudentIdUnique(studentId, allStudents));

            return studentId;
        }
        public static int GetStudentId(List<Student> allStudents)
        {
            string studentIdInput;
            int studentId;

            do
            {
                Console.Write("Введите номер студенческого билета (8 цифр): ");
                studentIdInput = Console.ReadLine();

                if (studentIdInput.ToLower() == "выход") return -1;
            }
            while (!IsValidStudentId(studentIdInput, out studentId));

            return studentId;
        }
        public static string GetInputSring(string message, bool isRedacting)
        {
            string input;
            do
            {
                Console.Write(message);
                input = Console.ReadLine();

                if (input.ToLower() == "выход") return null;

                if (isRedacting && string.IsNullOrWhiteSpace(input)) return string.Empty;

            } while (!ValidateStringField(input, isRedacting));

            return input;
        }
        public static DateTime GetDateInput(string fieldName, bool isRedacting)
        {
            DateTime dateOfBirth;
            string dateInput;

            while (true)
            {
                Console.Write($"{fieldName} (дд.мм.гггг): ");
                dateInput = Console.ReadLine();

                if (dateInput.ToLower() == "выход")
                {
                    return DateTime.MinValue;
                }

                if (ValidateDate(dateInput, out dateOfBirth, isRedacting))
                {
                    return dateOfBirth;
                }
            }
        }
        public static string GetInputGender(bool isRedacting)
        {
            string gender;
            while (true)
            {
                Console.Write("Введите пол (м/ж): ");
                gender = Console.ReadLine();
                if (gender.ToLower() == "выход") return null;

                if (isRedacting && string.IsNullOrWhiteSpace(gender)) return string.Empty;

                if (ValidateGender(gender, isRedacting)) return gender;

                Console.WriteLine("Некорректный ввод. Пол должен быть 'м' или 'ж'.");
            }
        }
        public static int GetInputCourse(bool isRedacting)
        {
            string input;
            int course;

            while (true)
            {
                Console.Write("Введите курс (1-6): ");
                input = Console.ReadLine();
                if (input.ToLower() == "выход") return -1;

                if (isRedacting && string.IsNullOrWhiteSpace(input)) return -2;

                if (ValidateCourse(input, out course, isRedacting)) return course;

                Console.WriteLine("Некорректный ввод. Курс должен быть числом от 1 до 6.");
            }
        }
        public static int GetInputExamCount(bool isRedacting, int currentExamCount)
        {
            string numberOfExamsInput;
            int numberOfExams = -1;

            do
            {
                Console.Write("Введите количество экзаменов: ");
                numberOfExamsInput = Console.ReadLine();

                if (numberOfExamsInput.ToLower() == "выход") return -1;

                if (string.IsNullOrWhiteSpace(numberOfExamsInput) && isRedacting)
                {
                    return currentExamCount;
                }

            } while (!ValidateExamCount(numberOfExamsInput, out numberOfExams));

            return numberOfExams;
        }
        public static int[] GetInputExamGrades(int numberOfExams, bool isRedacting)
        {
            string examGradesInput;
            int[] examGrades = null;

            do
            {
                Console.WriteLine("Введите оценки через запятую (например: 5,4,3): ");

                examGradesInput = Console.ReadLine();

                if (examGradesInput.ToLower() == "выход") return null;

                if (string.IsNullOrWhiteSpace(examGradesInput) && isRedacting == true)
                {
                    if (isRedacting)
                    {
                        return examGrades;
                    }
                }
            } while (!ValidateExamGrades(examGradesInput, numberOfExams, out examGrades));

            return examGrades;
        }
        public static bool IsValidStudentId(string input, out int studentId)
        {
            studentId = 0;
            if (input.Length == 8 && int.TryParse(input, out studentId))
            {
                return true;
            }

            Console.WriteLine("Номер студенческого билета должен содержать ровно 8 цифр.");
            return false;
        }
        public static bool IsStudentIdUnique(int studentId, List<Student> students)
        {
            foreach (var student in students)
            {
                if (student.StudentId == studentId)
                {
                    Console.WriteLine("Студенческий билет с таким номером уже существует.");
                    return false;
                }
            }
            return true;
        }
        public static bool ValidateStringField(string input, bool isRedacting)
        {
            if (isRedacting && string.IsNullOrWhiteSpace(input))
            {
                return true;
            }

            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Поле не может быть пустым.");
                return false;
            }

            bool previousWasSpace = false;
            bool previousWasHyphen = false;

            foreach (var c in input)
            {
                if (char.IsDigit(c))
                {
                    Console.WriteLine("Поле не должно содержать цифры.");
                    return false;
                }

                if (!char.IsLetter(c) && c != ' ' && c != '-')
                {
                    Console.WriteLine("Поле должно содержать только буквы, пробелы и знак '-'");
                    return false;
                }

                if (c == ' ')
                {
                    if (previousWasSpace)
                    {
                        Console.WriteLine("Поле не должно содержать более одного пробела подряд.");
                        return false;
                    }
                    previousWasSpace = true;
                    previousWasHyphen = false;
                }
                else if (c == '-')
                {
                    if (previousWasHyphen)
                    {
                        Console.WriteLine("Поле не должно содержать более одного знака '-' подряд.");
                        return false;
                    }
                    previousWasHyphen = true;
                    previousWasSpace = false;
                }
                else
                {
                    previousWasSpace = false;
                    previousWasHyphen = false;
                }
            }

            if (input.StartsWith(' ') || input.StartsWith('-') || input.EndsWith(' ') || input.EndsWith('-'))
            {
                Console.WriteLine("Поле не должно начинаться или заканчиваться пробелом или знаком '-'");
                return false;
            }

            return true;
        }
        public static bool ValidateDate(string input, out DateTime date, bool isRedacting)
        {
            date = default;
            if (isRedacting && string.IsNullOrWhiteSpace(input)) return true;
            if (DateTime.TryParseExact(input, "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out date))
                return true;
            Console.WriteLine("Некорректный ввод");
            return false;
        }
        public static bool ValidateGender(string input, bool isRedacting)
        {
            if (isRedacting && string.IsNullOrWhiteSpace(input)) return true;

            if (input.ToLower() == "м" || input.ToLower() == "ж") return true;

            Console.WriteLine("Пол должен быть указан как 'м' или 'ж'.");
            return false;
        }
        public static bool ValidateCourse(string input, out int course, bool isRedacting)
        {
            course = 0;
            if (isRedacting && string.IsNullOrWhiteSpace(input)) return true;

            if (int.TryParse(input, out course) && course >= 1 && course <= 6) return true;

            Console.WriteLine("Курс должен быть числом от 1 до 6.");
            return false;
        }
        public static bool ValidateExamCount(string input, out int count)
        {
            count = 0;
            if (int.TryParse(input, out count) && count >= 1 && count < 10) return true;
            Console.WriteLine("Количество экзаменов должно быть числом больше или равным 1.");
            return false;
        }
        public static bool ValidateExamGrades(string input, int numberOfExams, out int[] grades)
        {
            grades = null;
            var parts = input.Split(',');
            if (parts.Length != numberOfExams)
            {
                Console.WriteLine($"Введите ровно следующее количество оценок: {numberOfExams}.");
                return false;
            }

            grades = new int[parts.Length];
            for (int i = 0; i < parts.Length; i++)
            {
                if (!int.TryParse(parts[i].Trim(), out grades[i]) || grades[i] < 2 || grades[i] > 5)
                {
                    Console.WriteLine("Каждая оценка должна быть числом от 2 до 5.");
                    return false;
                }
            }
            return true;
        }
        public static void DisplayStudent(Student student)
        {
            Console.WriteLine($"Студенческий билет: {student.StudentId}");
            Console.WriteLine($"ФИО: {student.LastName} {student.FirstName} {student.Patronymic}");
            Console.WriteLine($"Дата рождения: {student.DateOfBirth.ToShortDateString()}");
            Console.WriteLine($"Пол: {student.Gender}");
            Console.WriteLine($"Факультет: {student.Faculty}");
            Console.WriteLine($"Специальность: {student.Specialty}");
            Console.WriteLine($"Курс: {student.Course}");
            Console.WriteLine($"Группа: {student.Group}");
            Console.WriteLine($"Количество экзаменов: {student.NumberOfExams}");
            Console.WriteLine($"Оценки: {string.Join(",", student.ExamGrades)}");
            Console.WriteLine(new string('-', 30));
        }
        public static string SelectFaculty(List<Faculty> faculties, string currentFaculty, bool isRedacting)
        {
            if (faculties == null || faculties.Count == 0)
            {
                Console.WriteLine("Список факультетов пуст. Добавьте хотя бы один факультет.");
                return null;
            }

            Console.WriteLine("Выберите факультет из списка:");

            for (int i = 0; i < faculties.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {faculties[i].FacultyName}");
            }

            Console.WriteLine("Введите номер факультета (или 'выход' для отмены):");

            if (isRedacting)
            {
                Console.WriteLine($"Оставьте поле пустым, чтобы оставить текущий факультет: {currentFaculty}");
            }

            string input = Console.ReadLine();

            if (input.ToLower() == "выход") return null;

            if (string.IsNullOrWhiteSpace(input))
            {
                if (isRedacting)
                {
                    return currentFaculty;
                }
                else
                {
                    Console.WriteLine("Поле не может быть пустым.");
                    return SelectFaculty(faculties, currentFaculty, isRedacting);
                }
            }

            if (int.TryParse(input, out int choice) && choice > 0 && choice <= faculties.Count)
            {
                return faculties[choice - 1].FacultyName;
            }
            else
            {
                Console.WriteLine("Некорректный ввод. Попробуйте еще раз.");
                return SelectFaculty(faculties, currentFaculty, isRedacting);
            }
        }
        public static string SelectSpecialty(List<Specialty> allSpecialties, string currentSpecialty, bool isRedacting)
        {
            if (allSpecialties == null || allSpecialties.Count == 0)
            {
                Console.WriteLine("Список специальностей пуст. Добавьте хотя бы одну специальность.");
                return null;
            }

            Console.WriteLine("Выберите специальность из списка:");

            for (int i = 0; i < allSpecialties.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {allSpecialties[i].SpecialtyName}");
            }

            Console.WriteLine("Введите номер специальности (или 'выход' для отмены):");

            if (isRedacting)
            {
                Console.WriteLine($"Оставьте поле пустым, чтобы оставить текущую специальность: {currentSpecialty}");
            }

            string input = Console.ReadLine();

            if (input.ToLower() == "выход") return null;

            if (string.IsNullOrWhiteSpace(input))
            {
                if (isRedacting)
                {
                    return currentSpecialty;
                }
                else
                {
                    Console.WriteLine("Поле не может быть пустым.");
                    return SelectSpecialty(allSpecialties, currentSpecialty, isRedacting);
                }
            }

            if (int.TryParse(input, out int choice) && choice > 0 && choice <= allSpecialties.Count)
            {
                return allSpecialties[choice - 1].SpecialtyName;
            }
            else
            {
                Console.WriteLine("Некорректный ввод. Попробуйте еще раз.");
                return SelectSpecialty(allSpecialties, currentSpecialty, isRedacting);
            }
        }
        public static void AddStudent(List<Student> allStudents, List<Faculty> allFaculties, List<Specialty> allSpecialties)
        {
            Console.WriteLine("Добавление нового студента");
            Console.WriteLine("Введите 'выход' для отмены");

            try
            {
                int studentId = GetUniqueStudentId(allStudents);
                if (studentId == -1) return;

                string lastName = GetInputSring("Введите фамилию: ", false);
                if (lastName == null) return;

                string firstName = GetInputSring("Введите имя: ", false);
                if (firstName == null) return;

                string patronymic = GetInputSring("Введите отчество: ", false);
                if (patronymic == null) return;

                DateTime dateOfBirth = GetDateInput("Введите дату рождения в формате ", false);
                if (dateOfBirth == DateTime.MinValue) return;

                string gender = GetInputGender(false);
                if (gender == null) return;

                string faculty = SelectFaculty(allFaculties, "", false);
                if (faculty == null) return;

                string specialty = SelectSpecialty(allSpecialties, "", false);
                if (specialty == null) return;

                int course = GetInputCourse(false);
                if (course == -1) return;

                string group = GetInputSring("Введите группу: ", false);
                if (group == null) return;

                int numberOfExams = GetInputExamCount(false, 0);
                if (numberOfExams == -1) return;

                int[] examGrades = GetInputExamGrades(numberOfExams, false);
                if (examGrades == null) return;

                Student newStudent = new Student(studentId, lastName, firstName, patronymic, dateOfBirth, gender, faculty, specialty, course, group, numberOfExams, examGrades);

                allStudents.Add(newStudent);

                Console.WriteLine("Студент успешно добавлен.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при добавлении студента: {ex.Message}");
            }
        }
        public static void EditStudentRecord(int StudentIndex, List<Student> allStudents, List<Faculty> allFaculties, List<Specialty> allSpecialties)
        {
            var studentToEdit = allStudents[StudentIndex];

            Console.WriteLine("Найден студент:");
            DisplayStudent(studentToEdit);

            Console.WriteLine("Введите данные для изменения. Оставьте поле пустым, чтобы оставить значение без изменений или напишите 'выход' для выхода в меню.");

            string lastName = GetInputSring("Введите фамилию: ", true);
            if (lastName != null)
            {
                if (!string.IsNullOrWhiteSpace(lastName))
                {
                    studentToEdit.LastName = lastName;
                }
            }
            else
            {
                return;
            }

            string firstName = GetInputSring("Введите имя: ", true);
            if (firstName != null)
            {
                if (!string.IsNullOrWhiteSpace(firstName))
                {
                    studentToEdit.FirstName = firstName;
                }
            }
            else
            {
                return;
            }

            string patronymic = GetInputSring("Введите отчество: ", true);
            if (patronymic != null)
            {
                if (!string.IsNullOrWhiteSpace(patronymic))
                {
                    studentToEdit.Patronymic = patronymic;
                }
            }
            else
            {
                return;
            }

            DateTime dateOfBirth = GetDateInput("Введите дату рождения в формате ", true);
            if (dateOfBirth != DateTime.MinValue) studentToEdit.DateOfBirth = dateOfBirth;

            string gender = GetInputGender(true);
            if (!string.IsNullOrWhiteSpace(gender)) studentToEdit.Gender = gender;

            string faculty = SelectFaculty(allFaculties, studentToEdit.Faculty, true);
            if (faculty != null)
            {
                studentToEdit.Faculty = faculty;
            }
            else
            {
                return;
            }

            string specialty = SelectSpecialty(allSpecialties, studentToEdit.Specialty, true);
            if (specialty != null)
            {
                studentToEdit.Specialty = specialty;
            }
            else
            {
                return;
            }

            int course = GetInputCourse(true);
            if (course == -1) return;
            else if (course != -2) studentToEdit.Course = course;

            string group = GetInputSring("Введите группу: ", true);
            if (group != null)
            {
                if (!string.IsNullOrWhiteSpace(group))
                {
                    studentToEdit.Group = group;
                }
            }
            else
            {
                return;
            }

            int currentExamCount = studentToEdit.NumberOfExams;
            int examsCount = GetInputExamCount(true, currentExamCount);

            if (examsCount != -1)
            {
                if (examsCount != currentExamCount)
                {
                    studentToEdit.NumberOfExams = examsCount;
                }
                int[] grades = GetInputExamGrades(examsCount, true);
                if (grades != null)
                {
                    allStudents[StudentIndex] = studentToEdit;
                    Console.WriteLine("Запись о студенте успешно обновлена.");
                    studentToEdit.ExamGrades = grades;
                }
                else
                {
                    allStudents[StudentIndex] = studentToEdit;
                    Console.WriteLine("Запись о студенте успешно обновлена.");
                    return;
                }
                allStudents[StudentIndex] = studentToEdit;
                Console.WriteLine("Запись о студенте успешно обновлена.");
            }
            else
            {
                return;
            }
            allStudents[StudentIndex] = studentToEdit;
        }
        public static void CheckExistenceOfFiles(params string[] paths)
        {
            for (int i = 0; i < paths.Length; i++)
            {
                if (!(File.Exists(paths[i])))
                {
                    File.Create(paths[i]).Close();
                }
            }
        }

        public static List<Student> ReadStudentsFromFile(string filePath)
        {
            var students = new List<Student>();

            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    try
                    {
                        var parts = line.Split('|');
                        if (parts.Length != 12)
                        {
                            Console.WriteLine($"Ошибка в строке: {line}");
                            continue;
                        }

                        int studentId = int.Parse(parts[0]);
                        string lastName = parts[1];
                        string firstName = parts[2];
                        string patronymic = parts[3];
                        DateTime dateOfBirth = DateTime.Parse(parts[4]);
                        string gender = parts[5];
                        string faculty = parts[6];
                        string specialty = parts[7];
                        int course = int.Parse(parts[8]);
                        string group = parts[9];
                        int numberOfExams = int.Parse(parts[10]);

                        string[] gradeStrings = parts[11].Split(';');

                        int[] examGrades = new int[gradeStrings.Length];
                        for (int i = 0; i < gradeStrings.Length; i++)
                        {
                            examGrades[i] = int.Parse(gradeStrings[i]);
                        }

                        students.Add(new Student(studentId, lastName, firstName, patronymic, dateOfBirth, gender, faculty, specialty,
                            course, group, numberOfExams, examGrades));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Ошибка при чтении строки: {line} | Ошибка: {ex.Message}");
                    }
                }
            }

            return students;
        }




        public static List<Faculty> ReadFacultiesFromFile(string facultiesFilePath)
        {
            var faculties = new List<Faculty>();
            var facultyNames = File.ReadAllLines(facultiesFilePath);

            for (int i = 0; i < facultyNames.Length; i++)
            {
                var facultyName = facultyNames[i].Trim();
                if (!string.IsNullOrEmpty(facultyName))
                {
                    var faculty = new Faculty(i + 1, facultyName);
                    faculties.Add(faculty);
                }
            }

            return faculties;
        }

        public static List<Specialty> ReadSpecialtiesFromFile(string specialtiesFilePath)
        {
            var specialties = new List<Specialty>();
            var specialtyLines = File.ReadAllLines(specialtiesFilePath);

            for (int i = 0; i < specialtyLines.Length; i++)
            {
                var specialtyName = specialtyLines[i].Trim();
                if (!string.IsNullOrEmpty(specialtyName))
                {
                    var specialty = new Specialty(i + 1, specialtyName);
                    specialties.Add(specialty);
                }
            }

            return specialties;
        }

        public static void SaveStudentsToFile(List<Student> allStudents, string filePath)
        {
            string result = "";
            foreach (var student in allStudents)
            {
                string examGrades = "";
                for (int i = 0; i < student.ExamGrades.Length; i++)
                {
                    examGrades += student.ExamGrades[i].ToString();
                    if (i < student.ExamGrades.Length - 1)
                    {
                        examGrades += ";";
                    }
                }

                string studentData = $"{student.StudentId}|{student.LastName}|{student.FirstName}|{student.Patronymic}|{student.DateOfBirth:dd.MM.yyyy}|{student.Gender}|{student.Faculty}|{student.Specialty}|{student.Course}|{student.Group}|{student.NumberOfExams}|{examGrades}";
                result += studentData + "\n";
            }
            File.WriteAllText(filePath, result.Trim());
        }

        public static void SaveSpecialtiesToFile(List<Specialty> allSpecialties, string filePath)
        {
            string result = "";
            foreach (var specialty in allSpecialties)
            {
                string specialtyData = $"{specialty.SpecialtyName}";
                result += specialtyData + "\n";
            }
            File.WriteAllText(filePath, result.Trim());
        }

        public static void SaveFacultiesToFile(List<Faculty> allFaculties, string filePath)
        {
            string result = "";
            foreach (var faculty in allFaculties)
            {
                string facultyData = $"{faculty.FacultyName}";

                result += facultyData + "\n";
            }
            File.WriteAllText(filePath, result.Trim());
        }
    }
}








