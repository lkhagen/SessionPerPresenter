//using System;
//using System.Collections.Generic;
//using System.Runtime.Remoting.Messaging;
//using NHibernate;
//using System.Web;


//namespace DataAccess.SessionHandling
//{
//    /// <summary>
//    /// Singleton class for managing NHibernate Sessions.
//    /// The core functionality is the SessionStore Dictionary that stores ISessions with a Guid key.
//    /// Methods for managing the sessions are also included.
//    /// </summary>
//    public class SessionManager
//    {
//        private IDictionary<Guid, ISession> SessionStore { get; set; }
//        private const string SESSION_KEY = "GUID_SESSION_KEY";
        

//        /// <summary>
//        /// Private constructor to enforce singleton
//        /// </summary>
//        private SessionManager()
//        {
//            SessionStore = new Dictionary<Guid, ISession>();
//        }
            

//        /// <summary>
//        /// Get a new or existing Session from the SessionStore
//        /// </summary>
//        /// <param name="sessionKey"></param>
//        /// <returns></returns>
//        public ISession GetSession(Guid sessionKey)
//        {
//            if (sessionKey == null) 
//                throw new ArgumentException("GetSession(): Null is an invalid arugment!");

//            if(SessionStore.ContainsKey(sessionKey))
//            {
//                // Re-initiate session if it is in an unusable state
//                if (SessionStore[sessionKey] == null)
//                {
//                    SessionStore[sessionKey] = SessionFactory.Instance.OpenSession();
//                }
//                else if (SessionStore[sessionKey].IsOpen == false)
//                {
//                    SessionStore[sessionKey].Dispose();
//                    SessionStore[sessionKey] = SessionFactory.Instance.OpenSession();
//                }
                
//                return SessionStore[sessionKey];
//            }
            
//            // Open new session, add it to SessionStore and return the session
//            ISession session = SessionFactory.Instance.OpenSession();
//            SessionStore.Add(sessionKey, session);
//            return session;
//        }

//        /// <summary>
//        /// Close session based on the Session
//        /// </summary>
//        /// <param name="sessionKey">The session key of the relevant session.</param>
//        public void CloseSession(Guid sessionKey)
//        {
//            ISession session = GetSession(sessionKey);
//            if (session != null && session.IsOpen)
//            {
//                SessionStore[sessionKey].Close();
//            }
//        }

//        /// <summary>
//        /// Removes session from SessionStore
//        /// </summary>
//        /// <param name="sessionKey">The session key of the relevant session.</param>
//        public void RemoveSession(Guid sessionKey)
//        {
//            if(SessionStore.ContainsKey(sessionKey))
//            {
//                // Dispose session if it exists
//                if(SessionStore[sessionKey] != null )
//                {
//                    SessionStore[sessionKey].Dispose();
//                }

//                SessionStore.Remove(sessionKey);
//            }
//        }

//        /// <summary>
//        /// Begin transaction on the spesified session
//        /// </summary>
//        /// <param name="sessionKey">The session key of the relevant session.</param>
//        public void BeginTransaction(Guid sessionKey)
//        {
//            GetSession(sessionKey).Transaction.Begin();
//        }

//        /// <summary>
//        /// Commit transaction on the specified session
//        /// </summary>
//        /// <param name="sessionKey">The session key of the relevant session.</param>
//        public void CommitTransaction(Guid sessionKey)
//        {
//            GetSession(sessionKey).Transaction.Commit();
//        }
        
//        /// <summary>
//        /// Rollback transaction on the specified session
//        /// </summary>
//        /// <param name="sessionKey">The session key of the relevant session.</param>
//        public void RollbackTransaction(Guid sessionKey)
//        {
//            GetSession(sessionKey).Transaction.Rollback();
//        }

//        /// <summary>
//        /// Does the spesified session have an open transaction
//        /// </summary>
//        /// <param name="sessionKey">The session key of the relevant session.</param>
//        /// <returns>True if the specific session has an open session.</returns>
//        public bool HasOpenTransaction(Guid sessionKey)
//        {
//            ISession session = GetSession(sessionKey);
//            if(session.Transaction != null && session.Transaction.IsActive)
//            {
//                return true;
//            }

//            return false;
//        }

//        /// <summary>
//        /// Evict an entity from the spesified Session
//        /// </summary>
//        /// <param name="sessionKey">The session key of the relevant session.</param>
//        /// <param name="entity">The entity to be evicted from the that session.</param>
//        public void Evict(Guid sessionKey, object entity)
//        {
//            ISession session = GetSession(sessionKey);
//            if(session.Contains(entity))
//            {
//                session.Evict(entity);
//            }
//        }
            
//        /// <summary>
//        /// Force a refresh of the data in a specific entity.
//        /// </summary>
//        /// <param name="sessionKey">The session key of the relevant session.</param>
//        /// <param name="entity">The entity to be refreshed from the database.</param>
//        public void Refresh(Guid sessionKey ,object entity)
//        {
//            ISession session = GetSession(sessionKey);
//            if(session.Contains(entity))
//            {
//                session.Refresh(entity);
//            }
//        }

//        /// <summary>
//        /// This is a thread-safe, lazy singleton.  See http://www.yoda.arachsys.com/csharp/singleton.html
//        /// for more details about its implementation.
//        /// </summary>
//        public static SessionManager Instance
//        {
//            get
//            {
//                return Nested.SessionManager;
//            }
//        }

//        /// <summary>
//        /// Assists with ensuring thread-safe, lazy singleton
//        /// </summary>
//        private class Nested
//        {
//            static Nested() { }
//            internal static readonly SessionManager SessionManager = new SessionManager();
//        }


//        /// <summary>
//        /// Returns true if application is running in a web context
//        /// </summary>
//        /// <returns></returns>
//        public bool IsInWebContext()
//        {
//            return HttpContext.Current != null;
//        }

//        /// <summary>
//        /// A default session key stored in the relevant context for compatability reasons
//        /// </summary>
//        public Guid DefaultSessionKey
//        {
//            get
//            {
//                // Get session key from HTTP/web context
//                if (IsInWebContext())
//                {
//                    if (HttpContext.Current.Items.Contains(SESSION_KEY) == false)
//                    {
//                        DefaultSessionKey = Guid.NewGuid();
//                    }
                    
//                    return (Guid)HttpContext.Current.Items[SESSION_KEY];
//                }
                
//                // Get session key CallContext when not running an web application
//                if(CallContext.GetData(SESSION_KEY) == null)
//                {
//                    DefaultSessionKey = Guid.NewGuid();
//                }

//                return (Guid)CallContext.GetData(SESSION_KEY);
//            }
//            set
//            {
//                if (IsInWebContext())
//                {
//                    HttpContext.Current.Items[SESSION_KEY] = value;
//                }
//                else
//                {
//                    CallContext.SetData(SESSION_KEY, value);
//                }
//            }
//        }

//    }
//}
