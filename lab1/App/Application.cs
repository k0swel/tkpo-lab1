using BaseRepositories.Interfaces;
using Course.Interfaces;
using Group.Interfaces;
using lab1.App;
using Student.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Teacher.Interfaces;
using static lab1.Application;

namespace lab1
{
    internal partial class Application
    {
        private ITeacherRepository _teacher_repository { get; set; }
        private StudentRepositoryInterface _student_repository { get; set; }
        private GroupRepositoryInterface _group_repository { get; set; }
        private ICourseRepository _course_repository { get; set; }
        private CourseEnrollmentRepository _course_enrollment_repository { get; set; }
        private CourseTranscriptRepositoryInterface _course_transcript_repository { get; set; }
        private static void _print_error(string text) {
            ConsoleColor prev = Console.BackgroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.Write("[!] ");
            Console.ForegroundColor = ConsoleColor.Gray; 
            Console.Error.WriteLine(text);
        }
        private static void _print_text(string text, bool print_new_line = true) {
            ConsoleColor prev = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            if (!print_new_line)
                Console.Write($"{text}");
            else
            {
                Console.WriteLine($"{text}");
            }
            Console.ForegroundColor = prev;
        }
        private static void _print_payload_text(string text, bool print_new_line = true) {
            Console.Write($"{text}{(print_new_line == true ? "\n" : " ")}");
        }
        private Application(RepositoryType type) {
            switch (type) {
                case RepositoryType.IN_MEMORY:
                    this._teacher_repository = new Teacher.Repositories.InMemoryTeacherRepository();
                    this._student_repository = new Student.Repositories.InMemoryStudentRepository();
                    this._group_repository = new Group.Repositories.InMemoryGroupInRepository();
                    this._course_repository = new Course.Repositories.InMemoryCourseRepository();
                    this._course_enrollment_repository = new Course.Repositories.InMemoryCourseEnrollmentRepository();
                    this._course_transcript_repository = new Course.Repositories.InMemoryStudentTranscriptRepository();
                    break;
                default:
                    throw new IncorrectKindOfRepostiryError($"Incorrect type of repository:  {type.ToString()}");
                    
            }
        }
        public static Application Initialize(RepositoryType type) {
            try
            {
                return new Application(type);
            }
            catch (IncorrectKindOfRepostiryError err) {
                _print_error(err.Message);
                Thread.Sleep(2000);
                Environment.Exit(1);
                return null;
            }
        }
        private static string[] _availableActions = new string[] {"Add a new student to the database", //0
            "Get Student by its ID",
            "Expel an existing student", // 1
            "Restore an existing student",
            "Add a new group", // 2
            "Get a group by its ID",
            "Disband an existing group", // 3
            "Add a course", // 4
            "Get a course by its ID",
            "Delete a course", // 5
            "Assign a student to course", // 6
            "Assign a group to course", // 7
            "Delete a student from course", // 8
            "Delete a group from course", // 9
            "Add a new teacher", // 10
            "Assign a teacher to course", // 11
            "Delete a teacher from course", // 12 
            "Set a mark"
            };
        public enum ACTIONS {
            CREATE_NEW_STUDENT = 1,
            GET_STUDENT_BY_ID,
            EXPEL_EXISTING_STUDENT,
            RESTORE_EXISTING_STUDENT,
            CREATE_NEW_GROUP,
            GET_GROUP_BY_ID,
            DISBAND_EXISTING_GROUP,
            ADD_COURSE,
            GET_COURSE_BY_ITS_ID,
            DELETE_COURSE,
            ASSIGN_A_STUDENT_TO_COURSE,
            ASSIGN_A_GROUP_TO_COURSE,
            DELETE_A_STUDENT_FROM_COURSE,
            DELEATE_A_GROUP_FROM_COURSE,
            CREATE_A_TEACHER,
            ASSIGN_A_TEACHER_TO_COURSE,
            DELETE_A_TEACHER_FROM_COURSE,
            SET_A_MARK,
            UNDEFINED_ACTION
        }
        public static void PrintHelloMessage()
        {
            var prev = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔══════════════════════════════════════════════════╗");
            Console.WriteLine("║   Welcome to the Faculty of the University!      ║");
            Console.WriteLine("╚══════════════════════════════════════════════════╝");
            Console.ResetColor();

            _print_text("Please choose what you want to do:", true);

            for (int i = 0; i < _availableActions.Count(); i++)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"\t[{i + 1}] ");
                Console.ForegroundColor = prev;
                Console.WriteLine(_availableActions[i]);
            }
            Console.WriteLine();
        }

        public static ACTIONS ChooseAction() {
            object? choose;
            bool successful_parse = Enum.TryParse(typeof(ACTIONS), _get_prompt(), out choose) ;
            if (!successful_parse) {
                return ACTIONS.UNDEFINED_ACTION;
            }
            return (ACTIONS)Convert.ToInt32(choose);
        }
        private static string? _get_prompt()
        {
            _print_text("> ", false);
            ConsoleColor prev = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Cyan;
            string? inputted_text = Console.ReadLine();
            Console.ForegroundColor = prev;
            return inputted_text;
        }
        private static string? _get_prompt(string text)
        {
            _print_text($"> {text}: ", false);
            ConsoleColor prev = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Cyan;
            string? inputted_text = Console.ReadLine();
            Console.ForegroundColor = prev;
            return inputted_text;
        }
        private void _handle_action(ACTIONS choose) {
            switch (choose)
            {
                case lab1.Application.ACTIONS.CREATE_NEW_STUDENT:
                    _createNewStudentHandler();
                    break;
                case lab1.Application.ACTIONS.GET_STUDENT_BY_ID:
                    _getStudentByItsIDHandler();
                    break;
                case lab1.Application.ACTIONS.EXPEL_EXISTING_STUDENT:
                    _expel_existing_student_handler();
                    break;
                case lab1.Application.ACTIONS.RESTORE_EXISTING_STUDENT:
                    _restore_existing_student_handler();
                    break;
                case lab1.Application.ACTIONS.CREATE_NEW_GROUP:
                    _create_new_group_handler();
                    break;
                case lab1.Application.ACTIONS.GET_GROUP_BY_ID:
                    _get_group_by_id();
                    break;
                case lab1.Application.ACTIONS.DISBAND_EXISTING_GROUP:
                    _disband_existing_group();
                    break;
                case lab1.Application.ACTIONS.ADD_COURSE:
                    _add_a_course_handler();
                    break;
                case lab1.Application.ACTIONS.GET_COURSE_BY_ITS_ID:
                    _get_a_course_by_id_handler();
                    break;
                case lab1.Application.ACTIONS.DELETE_COURSE:
                    _delete_a_course_handler();
                    break;
                case lab1.Application.ACTIONS.ASSIGN_A_STUDENT_TO_COURSE:
                    _assign_a_student_to_course_handler();
                    break;
                case lab1.Application.ACTIONS.ASSIGN_A_GROUP_TO_COURSE:
                    _assign_a_group_to_course_handler();
                    break;
                case lab1.Application.ACTIONS.DELETE_A_STUDENT_FROM_COURSE:
                    _delete_a_student_from_course_handler();
                    break;
                case lab1.Application.ACTIONS.DELEATE_A_GROUP_FROM_COURSE:
                    _delete_a_group_from_course_handler();
                    break;
                case lab1.Application.ACTIONS.CREATE_A_TEACHER:
                    _create_a_teacher_handler();
                    break;
                case lab1.Application.ACTIONS.ASSIGN_A_TEACHER_TO_COURSE:
                    _assign_a_teacher_to_course_handler();
                    break;
                case lab1.Application.ACTIONS.DELETE_A_TEACHER_FROM_COURSE:
                    _delete_a_teacher_from_course_handler();
                    break;
                case lab1.Application.ACTIONS.SET_A_MARK:
                    _set_a_mark_handler();

                //case lab1.Application.ACTIONS.FIRE_A_TEACHER:
                //    // todo
                //    break;
                //case lab1.Application.ACTIONS.ASSIGN_A_TEACHER_TO_COURSE:
                //    // todo
                    break;
                case ACTIONS.UNDEFINED_ACTION:
                    _print_error("Unavailable action. Choose another please");
                    choose = lab1.Application.ChooseAction();
                    _handle_action(choose);
                    break;
                default:
                    _print_error("Unavailable action. Choose another please");
                    choose = lab1.Application.ChooseAction();
                    _handle_action(choose);
                    break;
            }
        }

        public enum RepositoryType {
            IN_MEMORY,
            SQL,
        }
        public void EventLoop() {
            while (true)
            {
                try
                {
                    lab1.Application.PrintHelloMessage();
                    lab1.Application.ACTIONS choose = lab1.Application.ChooseAction();
                    _handle_action(choose);
                    //Console.Clear();
                }
                catch (Exception err)
                {
                    _print_error($"Ошибка: {err.Message}");
                }
            }
        }
    }
}
