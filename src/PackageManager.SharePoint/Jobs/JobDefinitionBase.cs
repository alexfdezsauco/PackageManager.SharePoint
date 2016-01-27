// --------------------------------------------------------------------------------------------------------------------
// <copyright file="JobDefinitionBase.cs" company="SANDs">
//   Copyright © 2016 SANDs. All rights reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace PackageManager.SharePoint.Jobs
{
    using System.Reflection;

    using Microsoft.SharePoint.Administration;

    /// <summary>
    ///     The job definition base.
    /// </summary>
    public class JobDefinitionBase : SPJobDefinition
    {
        #region Methods

        /// <summary>
        ///     The skip persisted store write check.
        /// </summary>
        private void SkipPersistedStoreWriteCheck()
        {
            var fieldInfo = typeof(SPPersistedObject).GetField("m_SkipPersistedStoreWriteCheck", BindingFlags.Instance | BindingFlags.NonPublic);
            if (fieldInfo != null)
            {
                fieldInfo.SetValue(this, true);
            }
        }

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="JobDefinitionBase" /> class.
        /// </summary>
        public JobDefinitionBase()
        {
            this.SkipPersistedStoreWriteCheck();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JobDefinitionBase"/> class.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="service">
        /// The service.
        /// </param>
        /// <param name="server">
        /// The server.
        /// </param>
        /// <param name="lockType">
        /// The lock type.
        /// </param>
        public JobDefinitionBase(string name, SPService service, SPServer server, SPJobLockType lockType)
            : base(name, service, server, lockType)
        {
            this.SkipPersistedStoreWriteCheck();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="JobDefinitionBase"/> class.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="webApplication">
        /// The web application.
        /// </param>
        /// <param name="server">
        /// The server.
        /// </param>
        /// <param name="lockType">
        /// The lock type.
        /// </param>
        public JobDefinitionBase(string name, SPWebApplication webApplication, SPServer server, SPJobLockType lockType)
            : base(name, webApplication, server, lockType)
        {
            this.SkipPersistedStoreWriteCheck();
        }

        #endregion
    }
}