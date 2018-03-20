namespace Ruiagf.Framework.Utils
{
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Data.SqlClient;

    public abstract class DataTransferObject
    {
        public void Fill(SqlDataReader dr)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(this);

            for (int i = 0; i < props.Count; ++i)
            {
                PropertyDescriptor prop = props[i];

                if (prop.IsReadOnly != true)
                {
                    try
                    {
                        if (dr[prop.Name].Equals(DBNull.Value) != true)
                        {
                            if (prop.PropertyType.Equals(dr[prop.Name].GetType()) != true)
                            {
                                prop.SetValue(this, prop.Converter.ConvertFrom(dr[prop.Name]));
                            }
                            else
                            {
                                prop.SetValue(this, dr[prop.Name]);
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }

        public void Fill(DataRow dr)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(this);

            for (int i = 0; i < props.Count; ++i)
            {
                PropertyDescriptor prop = props[i];

                if (prop.IsReadOnly != true)
                {
                    try
                    {
                        if (dr[prop.Name].Equals(DBNull.Value) != true)
                        {
                            if (prop.PropertyType.Equals(dr[prop.Name].GetType()) != true)
                            {
                                prop.SetValue(this, prop.Converter.ConvertFrom(dr[prop.Name]));
                            }
                            else
                            {
                                prop.SetValue(this, dr[prop.Name]);
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }
    }
}
