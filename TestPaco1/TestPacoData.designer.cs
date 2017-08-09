﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestPaco1
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="DatabasePaco")]
	public partial class TestPacoDataDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertEmpleado(Empleado instance);
    partial void UpdateEmpleado(Empleado instance);
    partial void DeleteEmpleado(Empleado instance);
    partial void InsertTelefono(Telefono instance);
    partial void UpdateTelefono(Telefono instance);
    partial void DeleteTelefono(Telefono instance);
    #endregion
		
		public TestPacoDataDataContext() : 
				base(global::TestPaco1.Properties.Settings.Default.DatabasePacoConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public TestPacoDataDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public TestPacoDataDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public TestPacoDataDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public TestPacoDataDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Empleado> Empleados
		{
			get
			{
				return this.GetTable<Empleado>();
			}
		}
		
		public System.Data.Linq.Table<Telefono> Telefonos
		{
			get
			{
				return this.GetTable<Telefono>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Empleados")]
	public partial class Empleado : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _ID;
		
		private string _Nombre;
		
		private string _Apellido;
		
		private string _Puesto;
		
		private EntitySet<Telefono> _Telefonos;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIDChanging(int value);
    partial void OnIDChanged();
    partial void OnNombreChanging(string value);
    partial void OnNombreChanged();
    partial void OnApellidoChanging(string value);
    partial void OnApellidoChanged();
    partial void OnPuestoChanging(string value);
    partial void OnPuestoChanged();
    #endregion
		
		public Empleado()
		{
			this._Telefonos = new EntitySet<Telefono>(new Action<Telefono>(this.attach_Telefonos), new Action<Telefono>(this.detach_Telefonos));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if ((this._ID != value))
				{
					this.OnIDChanging(value);
					this.SendPropertyChanging();
					this._ID = value;
					this.SendPropertyChanged("ID");
					this.OnIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Nombre", DbType="NVarChar(MAX) NOT NULL", CanBeNull=false)]
		public string Nombre
		{
			get
			{
				return this._Nombre;
			}
			set
			{
				if ((this._Nombre != value))
				{
					this.OnNombreChanging(value);
					this.SendPropertyChanging();
					this._Nombre = value;
					this.SendPropertyChanged("Nombre");
					this.OnNombreChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Apellido", DbType="NVarChar(MAX) NOT NULL", CanBeNull=false)]
		public string Apellido
		{
			get
			{
				return this._Apellido;
			}
			set
			{
				if ((this._Apellido != value))
				{
					this.OnApellidoChanging(value);
					this.SendPropertyChanging();
					this._Apellido = value;
					this.SendPropertyChanged("Apellido");
					this.OnApellidoChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Puesto", DbType="NVarChar(MAX) NOT NULL", CanBeNull=false)]
		public string Puesto
		{
			get
			{
				return this._Puesto;
			}
			set
			{
				if ((this._Puesto != value))
				{
					this.OnPuestoChanging(value);
					this.SendPropertyChanging();
					this._Puesto = value;
					this.SendPropertyChanged("Puesto");
					this.OnPuestoChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Empleado_Telefono", Storage="_Telefonos", ThisKey="ID", OtherKey="IDEmpleados")]
		public EntitySet<Telefono> Telefonos
		{
			get
			{
				return this._Telefonos;
			}
			set
			{
				this._Telefonos.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_Telefonos(Telefono entity)
		{
			this.SendPropertyChanging();
			entity.Empleado = this;
		}
		
		private void detach_Telefonos(Telefono entity)
		{
			this.SendPropertyChanging();
			entity.Empleado = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Telefonos")]
	public partial class Telefono : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _ID;
		
		private int _IDEmpleados;
		
		private string _Telefono1;
		
		private EntityRef<Empleado> _Empleado;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIDChanging(int value);
    partial void OnIDChanged();
    partial void OnIDEmpleadosChanging(int value);
    partial void OnIDEmpleadosChanged();
    partial void OnTelefono1Changing(string value);
    partial void OnTelefono1Changed();
    #endregion
		
		public Telefono()
		{
			this._Empleado = default(EntityRef<Empleado>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int ID
		{
			get
			{
				return this._ID;
			}
			set
			{
				if ((this._ID != value))
				{
					this.OnIDChanging(value);
					this.SendPropertyChanging();
					this._ID = value;
					this.SendPropertyChanged("ID");
					this.OnIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IDEmpleados", DbType="Int NOT NULL")]
		public int IDEmpleados
		{
			get
			{
				return this._IDEmpleados;
			}
			set
			{
				if ((this._IDEmpleados != value))
				{
					if (this._Empleado.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnIDEmpleadosChanging(value);
					this.SendPropertyChanging();
					this._IDEmpleados = value;
					this.SendPropertyChanged("IDEmpleados");
					this.OnIDEmpleadosChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="Telefono", Storage="_Telefono1", DbType="NVarChar(MAX) NOT NULL", CanBeNull=false)]
		public string Telefono1
		{
			get
			{
				return this._Telefono1;
			}
			set
			{
				if ((this._Telefono1 != value))
				{
					this.OnTelefono1Changing(value);
					this.SendPropertyChanging();
					this._Telefono1 = value;
					this.SendPropertyChanged("Telefono1");
					this.OnTelefono1Changed();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Empleado_Telefono", Storage="_Empleado", ThisKey="IDEmpleados", OtherKey="ID", IsForeignKey=true)]
		public Empleado Empleado
		{
			get
			{
				return this._Empleado.Entity;
			}
			set
			{
				Empleado previousValue = this._Empleado.Entity;
				if (((previousValue != value) 
							|| (this._Empleado.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Empleado.Entity = null;
						previousValue.Telefonos.Remove(this);
					}
					this._Empleado.Entity = value;
					if ((value != null))
					{
						value.Telefonos.Add(this);
						this._IDEmpleados = value.ID;
					}
					else
					{
						this._IDEmpleados = default(int);
					}
					this.SendPropertyChanged("Empleado");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591
