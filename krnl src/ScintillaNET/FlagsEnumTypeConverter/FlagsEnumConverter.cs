using System;
using System.ComponentModel;
using System.Reflection;

namespace FlagsEnumTypeConverter
{
	// Token: 0x02000002 RID: 2
	internal class FlagsEnumConverter : EnumConverter
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public FlagsEnumConverter(Type type) : base(type)
		{
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000205C File Offset: 0x0000025C
		public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
		{
			if (context != null)
			{
				Type type = value.GetType();
				string[] names = Enum.GetNames(type);
				Array values = Enum.GetValues(type);
				if (names != null)
				{
					PropertyDescriptorCollection propertyDescriptorCollection = new PropertyDescriptorCollection(null);
					for (int i = 0; i < names.Length; i++)
					{
						if ((int)values.GetValue(i) != 0 && names[i] != "All")
						{
							propertyDescriptorCollection.Add(new FlagsEnumConverter.EnumFieldDescriptor(type, names[i], context));
						}
					}
					return propertyDescriptorCollection;
				}
			}
			return base.GetProperties(context, value, attributes);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020D9 File Offset: 0x000002D9
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return context != null || base.GetPropertiesSupported(context);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020E7 File Offset: 0x000002E7
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return false;
		}

		// Token: 0x02000057 RID: 87
		protected class EnumFieldDescriptor : TypeConverter.SimplePropertyDescriptor
		{
			// Token: 0x0600035C RID: 860 RVA: 0x0000DB29 File Offset: 0x0000BD29
			public EnumFieldDescriptor(Type componentType, string name, ITypeDescriptorContext context) : base(componentType, name, typeof(bool))
			{
				this.fContext = context;
			}

			// Token: 0x0600035D RID: 861 RVA: 0x0000DB44 File Offset: 0x0000BD44
			public override object GetValue(object component)
			{
				return ((int)component & (int)Enum.Parse(this.ComponentType, this.Name)) != 0;
			}

			// Token: 0x0600035E RID: 862 RVA: 0x0000DB6C File Offset: 0x0000BD6C
			public override void SetValue(object component, object value)
			{
				int num;
				if ((bool)value)
				{
					num = ((int)component | (int)Enum.Parse(this.ComponentType, this.Name));
				}
				else
				{
					num = ((int)component & ~(int)Enum.Parse(this.ComponentType, this.Name));
				}
				component.GetType().GetField("value__", BindingFlags.Instance | BindingFlags.Public).SetValue(component, num);
				this.fContext.PropertyDescriptor.SetValue(this.fContext.Instance, component);
			}

			// Token: 0x0600035F RID: 863 RVA: 0x0000DBFA File Offset: 0x0000BDFA
			public override bool ShouldSerializeValue(object component)
			{
				return (bool)this.GetValue(component) != this.GetDefaultValue();
			}

			// Token: 0x06000360 RID: 864 RVA: 0x0000DC13 File Offset: 0x0000BE13
			public override void ResetValue(object component)
			{
				this.SetValue(component, this.GetDefaultValue());
			}

			// Token: 0x06000361 RID: 865 RVA: 0x0000DC27 File Offset: 0x0000BE27
			public override bool CanResetValue(object component)
			{
				return this.ShouldSerializeValue(component);
			}

			// Token: 0x06000362 RID: 866 RVA: 0x0000DC30 File Offset: 0x0000BE30
			private bool GetDefaultValue()
			{
				object obj = null;
				string name = this.fContext.PropertyDescriptor.Name;
				DefaultValueAttribute defaultValueAttribute = (DefaultValueAttribute)Attribute.GetCustomAttribute(this.fContext.PropertyDescriptor.ComponentType.GetProperty(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic), typeof(DefaultValueAttribute));
				if (defaultValueAttribute != null)
				{
					obj = defaultValueAttribute.Value;
				}
				return obj != null && ((int)obj & (int)Enum.Parse(this.ComponentType, this.Name)) != 0;
			}

			// Token: 0x170000FB RID: 251
			// (get) Token: 0x06000363 RID: 867 RVA: 0x0000DCAC File Offset: 0x0000BEAC
			public override AttributeCollection Attributes
			{
				get
				{
					return new AttributeCollection(new Attribute[]
					{
						RefreshPropertiesAttribute.Repaint
					});
				}
			}

			// Token: 0x04000824 RID: 2084
			private ITypeDescriptorContext fContext;
		}
	}
}
