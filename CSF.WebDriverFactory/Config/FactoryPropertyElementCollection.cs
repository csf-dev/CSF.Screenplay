using System;
using System.Configuration;

namespace CSF.WebDriverFactory.Config
{
  /// <summary>
  /// Represents a collection of <see cref="FactoryProperty"/> within a configuration file.
  /// </summary>
  public class FactoryPropertyElementCollection : ConfigurationElementCollection
  {
    internal const string PropertyName = "Property";

    /// <summary>
    /// Gets the configuration collection type.
    /// </summary>
    /// <value>The type of the collection.</value>
    public override ConfigurationElementCollectionType CollectionType
    {
      get
      {
        return ConfigurationElementCollectionType.BasicMapAlternate;
      }
    }

    /// <summary>
    /// Gets the name of the element.
    /// </summary>
    /// <value>The name of the element.</value>
    protected override string ElementName
    {
      get
      {
        return PropertyName;
      }
    }

    /// <summary>
    /// Determines whether the given string matches the <see cref="ElementName"/>.
    /// </summary>
    /// <returns><c>true</c> if the string matches the element name; otherwise, <c>false</c>.</returns>
    /// <param name="elementName">Element name.</param>
    protected override bool IsElementName(string elementName)
    {
      return elementName.Equals(PropertyName, StringComparison.InvariantCultureIgnoreCase);
    }

    /// <summary>
    /// Determines whether this instance is read only.
    /// </summary>
    /// <returns><c>true</c> if this instance is read only; otherwise, <c>false</c>.</returns>
    public override bool IsReadOnly()
    {
      return false;
    }

    /// <summary>
    /// Creates the new element.
    /// </summary>
    /// <returns>The new element.</returns>
    protected override ConfigurationElement CreateNewElement()
    {
      return new FactoryProperty();
    }

    /// <summary>
    /// Gets the element key.
    /// </summary>
    /// <returns>The element key.</returns>
    /// <param name="element">Element.</param>
    protected override object GetElementKey(ConfigurationElement element)
    {
      return ((FactoryProperty)(element)).Name;
    }

    /// <summary>
    /// Gets the <see cref="FactoryProperty"/> with the specified index.
    /// </summary>
    /// <param name="idx">Index.</param>
    public FactoryProperty this[int idx]
    {
      get
      {
        return (FactoryProperty)BaseGet(idx);
      }
    }
  }

  /// <summary>
  /// A single factory property, exposing a name/value pair, indicating a public settable property on the factory
  /// type and the value to be set into it.
  /// </summary>
  public class FactoryProperty : ConfigurationElement
  {
    const string NameConfigName = @"Name", ValueConfigName = @"Value";

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    /// <value>The name.</value>
    [ConfigurationProperty(NameConfigName, IsRequired = true)]
    public virtual string Name
    {
      get { return (string) this[NameConfigName]; }
      set { this[NameConfigName] = value; }
    }

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    /// <value>The value.</value>
    [ConfigurationProperty(ValueConfigName, IsRequired = true)]
    public virtual string Value
    {
      get { return (string) this[ValueConfigName]; }
      set { this[ValueConfigName] = value; }
    }
  }
}
