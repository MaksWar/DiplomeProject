using System.Collections.Generic;
using Additions.Extensions;
using MainMenu;

namespace Infrastructure.Services.DataAnalitycsService
{
	public class AnalyticsHelper
	{
		private static readonly Dictionary<GradationOfMark, List<string>> recommendationByGradationLogic =
			new Dictionary<GradationOfMark, List<string>>()
			{
				[GradationOfMark.Excellent] = new List<string>()
				{
					"To maintain an excellent level of logical thinking, continue to challenge yourself with a variety of complex puzzles, riddles, and problem-solving tasks.",
					"Engage in activities that require logical reasoning, such as playing strategy-based board games or participating in mind-stimulating discussions.",
					"Expand your logical thinking skills by exploring different branches of logic, such as deductive and inductive reasoning, and applying them to real-life situations.",
					"Consider joining a logic or puzzle-solving community to interact with like-minded individuals and participate in group challenges that foster continuous growth in logical thinking.",
					"Stay up to date with the latest advancements in logic and problem-solving techniques by reading books, attending workshops, or taking online courses in the field of logic and critical thinking.",
					"Regularly evaluate your own logical thinking abilities by setting personal goals, tracking your progress, and reflecting on your problem-solving strategies to identify areas for further improvement.",
					"Seek out opportunities to apply your logical thinking skills in practical situations, such as analyzing data, making informed decisions, or identifying patterns and trends in complex information.",
					"Embrace a growth mindset towards logical thinking, understanding that it is a skill that can be developed and enhanced through continuous practice, perseverance, and a willingness to embrace challenges."
				},
				[GradationOfMark.Good] = new List<string>()
				{
					"To further improve your logical thinking skills, actively seek out opportunities to apply logical reasoning in various scenarios and problem-solving tasks.",
					"Continue practicing logic puzzles and engaging in activities that stimulate your analytical thinking to enhance your logical reasoning abilities.",
					"Explore different types of logic puzzles, such as Sudoku, crosswords, or cryptic puzzles, to diversify your problem-solving techniques and broaden your logical thinking skills.",
					"Develop a habit of breaking down complex problems into smaller, manageable components and applying logical steps to solve them systematically.",
					"Challenge yourself to think outside the box and consider alternative perspectives or approaches to problem-solving, as it can sharpen your creative and logical thinking abilities.",
					"Collaborate with others on logic-based projects or team activities, as it can expose you to different problem-solving strategies and expand your logical thinking capabilities.",
					"Keep a journal or logbook of your logic puzzle-solving sessions, documenting the strategies you employed and reflecting on the effectiveness of your approach to identify areas for improvement.",
					"Stay mentally active and continuously engage your logical thinking skills by participating in brain-training exercises, such as logic games or mind teasers, during your leisure time."

				},
				[GradationOfMark.Satisfactory] = new List<string>()
				{
					"To enhance your level of logical thinking, regularly engage in exercises and puzzles that strengthen your analytical skills and reasoning abilities.",
					"Devote time to practicing logical thinking through activities like solving puzzles, analyzing patterns, and evaluating logical arguments.",
					"Explore online platforms or mobile applications that offer a variety of logic-based games and challenges to keep your mind sharp and improve your logical thinking skills.",
					"Take up a course or attend workshops on critical thinking and problem-solving techniques to expand your knowledge and enhance your logical reasoning abilities.",
					"Practice deductive and inductive reasoning by analyzing logical syllogisms, identifying valid and invalid arguments, and drawing logical conclusions from given premises.",
					"Develop a systematic approach to problem-solving by breaking down complex problems into smaller, manageable parts and applying logical steps to solve each component.",
					"Engage in debates or discussions that require logical reasoning and provide opportunities to defend your arguments with sound logic and evidence.",
					"Read books or articles on logic and reasoning to deepen your understanding of logical principles and apply them in various contexts.",
					"Seek feedback from others on your logical thinking skills and areas for improvement, and incorporate their suggestions into your practice."
				},
				[GradationOfMark.RequiresImprovement] = new List<string>()
				{
					"Focus on developing your logical thinking skills by actively seeking out challenging puzzles and problem-solving tasks that push your cognitive abilities.",
					"Work on honing your logical thinking abilities by practicing critical thinking exercises and logic-based activities that stretch your problem-solving capabilities.",
					"Break down complex problems into smaller, more manageable parts and practice solving each component systematically using logical reasoning.",
					"Explore different types of logic puzzles, such as riddles, brain teasers, and logic games, to expand your problem-solving techniques and improve your logical thinking skills.",
					"Engage in activities that require analytical thinking, such as Sudoku, chess, or strategy-based board games, to enhance your logical reasoning abilities.",
					"Seek out resources, such as online tutorials or books, that provide step-by-step guidance on improving logical thinking skills and apply the techniques to various challenges.",
					"Collaborate with others in group problem-solving activities to gain new perspectives and insights that can enhance your logical thinking approaches.",
					"Practice identifying logical fallacies and weak arguments to strengthen your ability to evaluate and critique reasoning in different contexts.",
					"Challenge yourself to think creatively and find innovative solutions to problems, incorporating logical thinking alongside other cognitive skills.",
					"Set specific goals for improving your logical thinking and track your progress over time, celebrating small achievements along the way."
				},
				[GradationOfMark.Unsatisfactory] = new List<string>()
				{
					"To improve your logical thinking, start by building a solid foundation of logical reasoning through basic exercises and introductory logic puzzles.",
					"Begin by strengthening your logical thinking skills with fundamental logic puzzles and gradually progress to more complex challenges as you develop your abilities.",
					"Engage in activities that promote critical thinking, such as analyzing arguments, identifying patterns, and solving basic logic problems.",
					"Practice breaking down complex problems into simpler components and working through each step systematically to improve your logical thinking process.",
					"Seek out educational resources, such as online tutorials or books, that provide guidance on logical thinking and reasoning techniques.",
					"Challenge yourself to think critically and question assumptions in everyday situations to sharpen your logical thinking abilities.",
					"Look for opportunities to apply logical reasoning in various contexts, such as decision-making, problem-solving, and evaluating information.",
					"Join logic-based communities or participate in logic competitions to interact with like-minded individuals and further develop your logical thinking skills.",
					"Take breaks during problem-solving sessions to reflect on your approach and identify areas for improvement in your logical thinking strategies.",
					"Seek feedback from others on your logical thinking processes and areas where you can enhance your reasoning abilities."
				},
			};

		public static string GetRecommendation(GradationOfMark gradationOfMark, Category category)
		{
			switch (category)
			{
				case Category.Logic:
					return recommendationByGradationLogic[gradationOfMark].GetRandomElement();

				case Category.Focus:
					break;
				case Category.Math:
					break;
			}

			return "";
		}
	}
}