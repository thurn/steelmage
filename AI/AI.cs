using System.Collections.Generic;

// When can you play this card?
public enum PlaySpeed {
  Land,
  Sorcery,
  Instant
}

public enum CardType {
  Instant,
  Sorcery,
  Land,
  Creature,
  Artifact,
  Enchantment,
  Planeswalker,
  Tribal
}

public struct ManaValue {
  public int genericValue;
  public int whiteValue;
  public int blueValue;
  public int blackValue;
  public int redValue;
  public int greenValue;
}

public enum Card {
  AridMesa,
  WoodedFoothills,
  RiftBolt,
  SearingBlaze,
  Skullcrack,
  MonasterySwiftspear
}

public enum Turn {
  Mine,
  Opponent
}

public enum GameStatus {
  ActiveStack,
  FirstMain,
  DeclareAttackers,
  DeclareBlockers,
  SecondMain,
  OpponentUpkeep,
  OpponentBeginCombat,
  OpponentEndOfTurn
}

public enum ActionType {
  PLAY_LAND,
  CAST_SPELL,
  TAP_LAND,
  ACTIVATE_ABILITY,
  DECLARE_AS_ATTACKER,
  DECLARE_AS_BLOCKER
}

public struct GameState {
  public Turn Turn;
  public GameStatus GameStatus;
  public List<Card> Hand;
  public ManaValue ManaPool;
  public HashSet<Card> Permanents;
}

public struct Action {
  public Card Card;
  public ActionType ActionType;
  public System.Enum ActionMode;
}

public class Game {
  GameState _gameState;
}

public class AI {
  // When can this card add options for its owner?
  public Dictionary<Card, PlaySpeed> PlaySpeeds = new Dictionary<Card, PlaySpeed> {
    {Card.AridMesa, PlaySpeed.Land},
    {Card.WoodedFoothills, PlaySpeed.Land},
    {Card.RiftBolt, PlaySpeed.Sorcery},
    {Card.SearingBlaze, PlaySpeed.Instant},
    {Card.Skullcrack, PlaySpeed.Instant},
    {Card.MonasterySwiftspear, PlaySpeed.Sorcery}
  };

  // Printed card types
  public Dictionary<Card, List<CardType>> CardTypes = new Dictionary<Card, List<CardType>> {
    {Card.AridMesa, new List<CardType> {CardType.Land}},
    {Card.WoodedFoothills, new List<CardType> {CardType.Land}},
    {Card.RiftBolt, new List<CardType> {CardType.Sorcery}},
    {Card.SearingBlaze, new List<CardType> {CardType.Instant}},
    {Card.Skullcrack, new List<CardType> {CardType.Instant}},
    {Card.MonasterySwiftspear, new List<CardType> {CardType.Creature}}
  };
  
  // Printed mana costs, not including alternate costs
  public Dictionary<Card, ManaValue> ManaCosts = new Dictionary<Card, ManaValue> {
    {Card.RiftBolt, new ManaValue {redValue = 1, genericValue = 2}},
    {Card.SearingBlaze, new ManaValue {redValue = 2}},
    {Card.Skullcrack, new ManaValue {redValue = 1, genericValue = 1}},
    {Card.MonasterySwiftspear, new ManaValue {redValue = 1}}
  };

  public static void Main() {
    System.Console.WriteLine("Hello, World!");
    AridMesaCard aridMesa = CardRegistry.Cards[Card.AridMesa] as AridMesaCard;
    System.Console.WriteLine("Hello, " + aridMesa.Hello());
  }
}

public class CardRegistry {
  public static Dictionary<Card, AbstractCard> Cards = new Dictionary<Card, AbstractCard> {
    {Card.AridMesa, new AridMesaCard()},
    {Card.WoodedFoothills, new WoodedFoothillsCard()},
    {Card.RiftBolt, new RiftBoltCard()},
    {Card.SearingBlaze, new SearingBlazeCard()},
    {Card.Skullcrack, new SkullcrackCard()},
    {Card.MonasterySwiftspear, new MonasterySwiftspearCard()},                        
  };
}

public abstract class AbstractCard {
  public abstract Card GetCard();
}

public class AridMesaCard : AbstractCard {
  public override Card GetCard() {
    return Card.AridMesa;
  }

  public string Hello() {
    return "AridMesa";
  }
}

public class WoodedFoothillsCard : AbstractCard {
  public override Card GetCard() {
    return Card.WoodedFoothills;
  }
}

public class RiftBoltCard : AbstractCard {
  public override Card GetCard() {
    return Card.RiftBolt;
  }
}

public class SearingBlazeCard : AbstractCard {
  public override Card GetCard() {
    return Card.SearingBlaze;
  }
}

public class SkullcrackCard : AbstractCard {
  public override Card GetCard() {
    return Card.Skullcrack;
  }
}

public class MonasterySwiftspearCard : AbstractCard {
  public override Card GetCard() {
    return Card.MonasterySwiftspear;
  }
}