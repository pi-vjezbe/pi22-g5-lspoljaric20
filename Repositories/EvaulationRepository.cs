﻿using DBLayer;
using Evaluation_Manager.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evaluation_Manager.Repositories
{
public class EvaulationRepository
{

        public static Evaulation GetEvaluation(Student student, Activity activity)
{
            Evaulation evaluation = null;
            string sql = $"SELECT * FROM Evaluations WHERE IdStudents = {student.Id} AND IdActivities = {activity.Id}";
            DB.OpenConnection();
            var reader = DB.GetDataReader(sql);
            if (reader.HasRows)
            {
                reader.Read();
                evaluation = CreateObject(reader);
                reader.Close();
            }

            DB.CloseConnection();
            return evaluation;
        }

        public static List<Evaulation> GetEvaluations(Student student)
        {
            List<Evaulation> evaluations = new List<Evaulation>();

            string sql = $"SELECT * FROM Evaluations WHERE IdStudents = {student.Id}";
            DB.OpenConnection();
            var reader = DB.GetDataReader(sql);

            while (reader.Read())
            {
                Evaulation evaluation = CreateObject(reader);
                evaluations.Add(evaluation);
            }

            reader.Close();
            DB.CloseConnection();

            return evaluations;
        }

        private static Evaulation CreateObject(SqlDataReader reader)
        {
            int idActivities = int.Parse(reader["IdActivities"].ToString());
            var activity = ActivityRepository.GetActivity(idActivities);

            int idStudents = int.Parse(reader["IdStudents"].ToString());
            var student = StudentRepository.GetStudent(idStudents);

            int idTeachers = int.Parse(reader["IdTeachers"].ToString());
            var teacher = TeacherRepositories.GetTeacher(idTeachers);

            DateTime evaluationDate = DateTime.Parse(reader["EvaluationDate"].ToString());
            int points = int.Parse(reader["Points"].ToString());

            var evaluation = new Evaulation
            {
                Activity = activity,
                Student = student,
                Evaluator = teacher,
                EvaulationDate = evaluationDate,
                Points = points
            };

            return evaluation;
        }

        public static void UpdateEvaluation(Evaulation evaluation, Teacher teacher, int points)
        {
            string sql = $"UPDATE Evaluations SET IdTeachers = {teacher.Id}, Points = { points}, EvaluationDate = GetDate() WHERE IdActivities = {evaluation.Activity.Id} AND IdStudents = { evaluation.Student.Id }";

            DB.OpenConnection();
            DB.ExecuteCommand(sql);
            DB.CloseConnection();
        }

        public static void InsertEvaluation(Student student, Activity activity, Teacher teacher, int points)
        {
            string sql = $"INSERT INTO Evaluations (IdActivities, IdStudents, IdTeachers, EvaluationDate, Points) VALUES ({activity.Id}, {student.Id}, {teacher.Id}, GETDATE(), {points})";
            DB.OpenConnection();
            DB.ExecuteCommand(sql);
            DB.CloseConnection();
        }
    }

}
