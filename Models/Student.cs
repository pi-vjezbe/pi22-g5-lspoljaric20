using Evaluation_Manager.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evaluation_Manager.Models
{
    public class Student : Person
    {
        public int Grade { get; set; }

        public List<Evaluation> GetEvaluations()
        {
            return EvaluationRepository.GetEvaluations(this);
        }

        public int CalculateTotalPoints()
        {
            int totalPoints = 0;
            foreach (var evaluation in GetEvaluations()) 
            {
                totalPoints += evaluation.Points;
            }
            return totalPoints;
        }

        public bool HasSignature()
        {
            bool hasSignature = true;
            if (IsEvaluationComplete() == true)
            {
                foreach (var evaluation in GetEvaluations())
                {
                    if (evaluation.IsSufficientForSignature()==false)
                    {
                        hasSignature = false;
                        break;
                    }
                    else
                    {
                        hasSignature = false;
                    }
                }
            
            }
            return hasSignature;
        }

        public bool HasGrade()
        {
            bool hasgrade = true;
            if (IsEvaluationComplete() == true)
            {
                foreach (var evaluation in GetEvaluations())
                {
                    if (evaluation.IsSufficientForGrade() == false)
                    {
                        hasgrade = false;
                        break;
                    }
                    else
                    {
                        hasgrade = false;
                    }
                }

            }
            return hasgrade;
        }

        private bool IsEvaluationComplete()
        {
            var evaluations = GetEvaluations();
            var activities = ActivityRepository.GetActivities();

            return evaluations.Count == activities.Count;
        }

        public int CalculateGrade()
        {
            int grade = 0;
            if (HasGrade())
            {
                int totalPoints = CalculateTotalPoints();
                if(totalPoints>= 91)
                {
                    grade = 5;
                }
                else if (totalPoints >=76 && totalPoints <91 )
                {
                    grade = 4;
                }
                else if(totalPoints >= 61 && totalPoints < 76)
                {
                    grade = 3;
                }
                else if (totalPoints >= 50 && totalPoints < 61)
                {
                    grade = 2;
                }
                else 
                {
                    grade = 1;
                }
            }
            return grade;
        }
    }
}
