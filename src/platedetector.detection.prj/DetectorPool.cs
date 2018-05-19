using System.Collections.Concurrent;
using System.Threading;

namespace Platedetector.Detection
{
    /// <summary> Пул, позволяющий использовать несколько детекторов одновременно. </summary>
    public sealed class DetectorPool<T> where T: IDetectionAlgProvider
    {
        #region Data

        /// <summary> Коллекция детекторов. </summary>
        private readonly ConcurrentBag<IDetector> _bag;

        /// <summary> Количество детекторов по умолчанию. </summary>
        private const int _defaultCapacity = 10;

        #endregion
        
        #region .ctor

        /// <summary> Создает <see cref="DetectorPool{T}"/>. </summary>
        /// <param name="capacity"> Количество детекторов в пуле. </param>
        public DetectorPool(int capacity = _defaultCapacity)
        {
            Capacity = capacity;
            SyncRoot = new object();

            _bag = new ConcurrentBag<IDetector>();
            for(int i = 0; i < capacity; i++)
            {
                _bag.Add(new Detector(new AlgManager(default(T))));
            }
        }

        #endregion

        #region Properties

        public int Capacity { get; }

        public object SyncRoot { get; }
        #endregion

        #region Methods

        /// <summary> Добавляет детектор в пул, если есть место. </summary>
        /// <param name="detector"> Детектор </param>
        public void TryAdd(IDetector detector)
        {
            Monitor.Enter(SyncRoot);
            try
            {
                if(_bag.Count <= Capacity)
                {
                    _bag.Add(detector);
                    Monitor.PulseAll(SyncRoot);
                }
            }
            finally
            {
                Monitor.Exit(SyncRoot);
            }
        }

        /// <summary> Вытащить свободный детектор из пула. </summary>
        /// <returns> Возвращает свободный детектор. </returns>
        public IDetector TryTake()
        {
            Monitor.Enter(SyncRoot);
            try
            {
                if (_bag.Count <= 0)
                {
                    Monitor.Wait(SyncRoot);
                }

                _bag.TryTake(out IDetector detector);
                return detector;
            }
            finally
            {
                Monitor.Exit(SyncRoot);
            }
        }
        #endregion
    }
}
