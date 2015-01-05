using System;
using System.Collections.Generic;
using System.Linq;

namespace EveList8._1.Common
{
    public static class Messenger<T>
    {
        private static readonly List<Action<T>> Actions;

        static Messenger()
        {
            Actions = new List<Action<T>>();
        }

        public static void Send(T message)
        {
            var yt = Actions.Count;
            for (int index = 0; index < yt; index++)
            {
                var action = Actions[index];
                action.Invoke(message);
            }
        }

        public static void Register(Action<T> action)
        {
            if(!Actions.Contains(action))
                Actions.Add(action);
        }
    }

    class NavigationMessage
    {
        public NavigationMessage(string pageName, object parametrQuery)
        {
            PageName = pageName;
            ParametrQuery = parametrQuery;
        }

        public string PageName { get; set; }
        public object ParametrQuery { get; set; }
    }

}
