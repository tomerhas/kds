using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KdsService
{
    public abstract class BaseBlockingExecuter<T>
    {
        protected BlockingCollection<T> _tasksToExecuteCollection;

        public BaseBlockingExecuter()
        {
            _tasksToExecuteCollection = new BlockingCollection<T>();
        }

        public virtual void Init(int numWorkerThreads)
        {
            //Call the inhertining init method
            InitInternal();
            //Start numWorkerThreads threads that will each listen on the blocking queue
            for (int i = 0; i < numWorkerThreads; i++)
            {
                Task.Factory.StartNew(() => SingleConsumer());
            }
        }

        public void Add(T item)
        {
            _tasksToExecuteCollection.Add(item);
        }

        public void AddRange(List<T> list)
        {
            list.ForEach(t => _tasksToExecuteCollection.Add(t));
        }

        public int Count()
        {
            return _tasksToExecuteCollection.Count();
        }

        //Function to be executed by a single thread
        protected void SingleConsumer()
        {
            bool shouldContinue = true;
            //Continue running all the time
            while (shouldContinue)
            {
                //This is a blocking call
                var task = _tasksToExecuteCollection.Take();
                //Call the override start task
                BaseResult result = null;
                try
                {
                    result = TaskStartExecute(task);
                }
                catch (Exception ex)
                {
                    //Report exception to application and continue working or not according to result from implementing class
                    if (TaskException(task, ex) == false)
                    {
                        shouldContinue = false;
                    }
                    result.IsSucceeded = false;
                    result.ErrorMsg = ex.ToString();
                }
                //Call the override complete task
                TaskCompletExecute(task, result);
            }
        }


        protected abstract BaseResult TaskStartExecute(T item);
        protected abstract void TaskCompletExecute(T task, BaseResult result);
        //The return value informs if required to continue with the execution or stop
        protected abstract bool TaskException(T task, Exception ex);
        protected abstract void InitInternal();
    }

    public class BaseResult
    {
        public bool IsSucceeded { get; set; }
        public string ErrorMsg { get; set; }
    }
}





 

