//An interface gives other classes or programs a few definitions
/// <summary>
/// those definitions can be coded similarly, or abstracted to some degree, 
///  yet anyone that uses this interface will have access to all the properties/methods written regardless of class restrictions
///  (e.g.) A Ground-bug class and a Flying-bird class can use IMove to, well, move! It simplifies and makes code reusable by ALOT
/// Without an interfacem different classes with zero-relation would need to inherinted from some mega-uber class.
///  but that would get tedious, repetitive and unmaginable the more complicated things get 
///  (lets say Bugs, Birds, and Rocks needed to all move, what would teh ancestor/parent class be, "Things" "Moveable")
///  But what about Cars, or plain ol Metal that inherent from say "Automobile" class or "Rare Metal" classes, would they also need to inherent?
///  Where does the line of inherentince begin and end, and REMEMBER changes to the parent class alters the children using le parent's methods.
///  Would you want to constantly have to go into sepreate children and type "Override" what if you actually WANT the parent's changes to be reflected in the children.
/// See, all that and more is why interfaces are cool. They're seperated from inheritances, parent-children relationships, and just provide an AMBLE book/tome/dictionary
///  for anyone to use, regardless of relation!
/// </summary>
public interface IMove
{
    //You don't need to use accesss specifiers/modifiers
    /// <summary>
    ///     like private or protected, because by default, interfaces are public
    /// </summary>

    //Anythat implements IMove will be able to access its Speed value
    float Speed { get; }
}

