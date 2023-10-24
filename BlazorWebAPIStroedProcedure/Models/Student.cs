using System;
using System.Collections.Generic;
using CsvHelper.Configuration.Attributes;
using System.ComponentModel.DataAnnotations;

namespace BlazorWebAPIStroedProcedure.Models
{

    public class Student
    {
        [Name("Student_ID")]
        [RegularExpression(@"^STDN\d{5}$", ErrorMessage = "StudentID must be in STDN00000 format.")]
        public string StudentId { get; set; } = null!;

        [Name("gender")]
        [RegularExpression(@"^[MF]$", ErrorMessage = "Gender must be 'M' or 'F'.")]
        public char Gender { get; set; }

        [Name("NationalITy")]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Nationality must only contain Alphabets and White Space.")]
        public string? NationalIty { get; set; }

        [Name("PlaceofBirth")]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "PlaceOfBirth must only contain Alphabets and White Space.")]
        public string? PlaceofBirth { get; set; }

        [Name("StageID")]
        [RegularExpression("^(lowerlevel|MiddleSchool|HighSchool)$", ErrorMessage = "StageID must be 'loverlevel' or 'MiddleSchool' or 'HighSchool'.")]
        public string? StageId { get; set; }

        [Name("GradeID")]
        [RegularExpression(@"^G-\d{2}$", ErrorMessage = "GradeID must be G-00 format.")]
        public string? GradeId { get; set; }

        [Name("SectionID")]
        [RegularExpression(@"^[A-C]$", ErrorMessage = "SectionID must be 'A' or 'C'.")]
        public char SectionId { get; set; }

        [Name("Topic")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Topic must only contain Alphabets.")]
        public string? Topic { get; set; }

        [Name("Semester")]
        [RegularExpression(@"^[FS]$", ErrorMessage = "Semester must be 'F' or 'S'.")]
        public char Semester { get; set; }

        [Name("Relation")]
        [RegularExpression("^(Father|Mum)$", ErrorMessage = "Relation must be 'Father' or 'Mum'.")]
        public string? Relation { get; set; }

        [Name("raisedhands")]
        [RegularExpression(@"^(?:\d{1,3}|999)$", ErrorMessage = "RaisedHands must be a number between 0 - 999.")]
        public int? Raisedhands { get; set; }

        [Name("VisITedResources")]
        [RegularExpression(@"^(?:\d{1,3}|999)$", ErrorMessage = "VisitedResources must be a number between 0 - 999.")]
        public int? VisItedResources { get; set; }

        [Name("AnnouncementsView")]
        [RegularExpression(@"^(?:\d{1,3}|999)$", ErrorMessage = "AnnouncementsView must be a number between 0 - 999.")]
        public int? AnnouncementsView { get; set; }

        [Name("Discussion")]
        [RegularExpression(@"^(?:\d{1,3}|999)$", ErrorMessage = "Discussion must be a number between 0 - 999.")]
        public int? Discussion { get; set; }

        [Name("ParentAnsweringSurvey")]
        [RegularExpression("^(Yes|No)$", ErrorMessage = "ParentAnsweringSurvey must be 'Yes' or 'No'.")]
        public string? ParentAnsweringSurvey { get; set; }

        [Name("ParentschoolSatisfaction")]
        [RegularExpression("^(Good|Bad)$", ErrorMessage = "ParentSchoolSatisfaction must be 'Good' or 'Bad'.")]
        public string? ParentschoolSatisfaction { get; set; }

        [Name("StudentAbsenceDays")]
        [RegularExpression("^(Under-7|Above-7)$", ErrorMessage = "StudentAbsenceDays must be 'Under-7' or 'Above-7'.")]
        public string? StudentAbsenceDays { get; set; }

        [Name("Student Marks")]
        [RegularExpression(@"^(?:\d{1,3}|100)$", ErrorMessage = "StudentMarks must be a number between 0 - 100.")]
        public int? StudentMarks { get; set; }

        [Name("Class")]
        [RegularExpression(@"^[MLH]$", ErrorMessage = "Class must be 'M' or 'L' or 'H'.")]
        public char Class { get; set; }
    }

}
