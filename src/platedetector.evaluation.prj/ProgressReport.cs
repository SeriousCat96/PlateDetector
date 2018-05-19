namespace Platedetector.Evaluation
{
    /// <summary> Предоставляет аргументы для отчета о процессе выполнения. </summary>
    public class ProgressReport
    {
        /// <summary> Текущий файл </summary>
        public string File { get; set; }

        /// <summary> Текущая позиция в списке файлов </summary>
        public int CurPosition { get; set; }

        /// <summary> Количество файлов </summary>
        public int ItemsCount { get; set; }
    }
}