﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace KdsTaskManager
{
    public class ProgramCommand : Command
    {

        private Type _Type;
        private MethodInfo _MethodInfo;
        private object[] _parameters;
        private ReferenceDefinitions _oReferenceDefinitions;
        private Reference _Reference;
        private Assembly _Dllfile;
        private ConstructorInfo _ConstructorInfo;


        public ProgramCommand(Action ActionToExecute)
        {
            _ActionToExecute = ActionToExecute;
            _parameters = null;
            try
            {
                _oReferenceDefinitions = ReferenceDefinitions.GetInstance();
                _Reference = _oReferenceDefinitions.ReferenceList.Find(refItem => refItem.Name == _ActionToExecute.LibraryName);
                if (_Reference == null)
                    throw new Exception("Reference path not defined in xml for library " + _ActionToExecute.LibraryName);
                _Dllfile = Assembly.LoadFile(_Reference.FullPath);
                if (_Dllfile == null)
                    throw new Exception("Problem in loading the assembly reference " + _Reference.FullPath);
                _Type = _Dllfile.GetType(_ActionToExecute.LibraryName);
                _ConstructorInfo = _Type.GetConstructor(Type.EmptyTypes);
                if (_ConstructorInfo == null)
                    throw new Exception("Object " + _ActionToExecute.LibraryName + " creation failed or is not valid");
            }
            catch
            {
                throw;
            }

        }

        /// <remarks>Fill the Type and MethodInfo</remarks>
        private void CreateMethodToExecute()
        {
            try
            {

                _MethodInfo = _Type.GetMethod(_ActionToExecute.CommandName);
                if (_MethodInfo != null)
                {
                    if (_ActionToExecute.Parameters.Count > 0)
                        _parameters = GetParametersOfFunction();
                }
                else throw new Exception("MethodInfo:" + _ActionToExecute.CommandName + " is not valid");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal Type GetParameterType(int dataType)
        {
            string type = string.Empty;
            switch (dataType)
            {
                case 1:
                    type = "System.String";
                    break;
                case 2:
                    type = "System.Int16";
                    break;
                case 3:
                    type = "System.Int32";
                    break;
                case 4:
                    type = "System.Int64";
                    break;
                case 5:
                    type = "System.DateTime";
                    break;
                case 6:
                    type = "System.Decimal";
                    break;
            }
            return Type.GetType(type);
        }

        private object[] GetParametersOfFunction()
        {
            object[] Obj = new object[_ActionToExecute.Parameters.Count];
            try
            {
                int Counter = 0;
                _ActionToExecute.Parameters.ForEach((ParameterItem) =>
                                        {
                                            Obj[Counter] = Convert.ChangeType(ParameterItem.Value, GetParameterType(ParameterItem.Type));
                                            Counter++;
                                        });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Obj;

        }

        protected override void Execute()
        {
            try
            {
                CreateMethodToExecute();
                _MessageStart = new Message(_ActionToExecute, TypeStatus.Running, string.Empty, DateTime.Now, DateTime.Now);
                _MessageStart.UpdateTaskLog();
                object obj = _ConstructorInfo.Invoke(new object[] { });
                _ActionResult = (bool)_MethodInfo.Invoke(obj, _parameters);
                _MessageEnd = new Message(_ActionToExecute, TypeStatus.Success, string.Empty, DateTime.Now, DateTime.Now);
                _MessageEnd.UpdateTaskLog();
            }
            catch (Exception ex)
            {
                _ActionResult = false;
                throw ex;
            }

        }
    }


}
