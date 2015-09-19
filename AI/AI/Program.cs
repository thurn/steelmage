using System;
using System.Collections.Generic;
using System.Linq;

namespace AI {
  public static class Empty
  {
    public static readonly ValueType Value = default(ValueType);
  }

  // When can you play this card?
  public enum PlaySpeed {
    Land,
    Sorcery,
    Instant
  }

  public enum Color {
    White,
    Blue,
    Black,
    Red,
    Green,
    Colorless
  }

  public enum BasicLandType {
    Plains,
    Island,
    Swamp,
    Mountain,
    Forest
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

  public enum CardSupertype {
    Legendary,
    Basic
  }

  public enum CardSubtype {
    Spirit,
    Arcane,
    Goblin,
    Scout,
    Human,
    Monk,
    Wizard
  }

  public struct ManaValue {
    public bool Absent;
    public byte GenericValue;
    public byte WhiteValue;
    public byte BlueValue;
    public byte BlackValue;
    public byte RedValue;
    public byte GreenValue;
  }

  public struct PowerAndToughness {
    public bool Absent;
    public byte Power;
    public byte Toughness;
  }

  public enum Turn {
    Mine,
    Opponent
  }

  public enum GameStatus {
    ActiveStack,
    FirstMain,
    DeclareAttackers,
    AfterDeclareAttackers,
    AfterDeclareBlockers,
    SecondMain,
    OpponentUpkeep,
    OpponentBeginCombat,
    OpponentAfterDeclareAttackers,
    OpponentDeclareBlockers,
    OpponentAfterDeclareBlockers,
    OpponentEndOfTurn
  }

  public enum Zone {
    Hand,
    Battlefield,
    Graveyard,
    Exile,
    Stack,
    Library
  }

  public enum ActionType {
    Unknown,
    PlayLand,
    CastSpell,
    TapLandForMana,
    ActivateAbility,
    DeclareAsAttacker,
    DeclareAsBlocker
  }

  public enum CounterType {
    PlusOnePlusOne,
    MinusOneMinusOne,
    Loyalty
  }

  public class Permanent {
    public int Id;
    public Card Card;
    public bool Tapped;
    public Dictionary<CounterType, int> Counters;
    public bool ControlledSinceStartOfTurn;
  }

  public class GameState {
    public Turn Turn;
    public GameStatus GameStatus;
    public List<Card> Hand;
    public ManaValue ManaPool;
    public Dictionary<int, Permanent> Permanents;
    public List<Card> Library;
    public HashSet<Card> Decklist;
    public byte LifeTotal;
    public byte OpponentLifeTotal;
    public byte LandPlaysThisTurn;
    public byte MaxLandPlaysPerTurn;
  }

  public struct Action {
    public int Source;
    public ActionType ActionType;
    public System.ValueType ActionChoices;
  }

  public class Game {
    GameState _gameState;
  }

  public enum Card {
    Mountain,
    StompingGround,
    SacredFoundry,
    AridMesa,
    WoodedFoothills,
    RiftBolt,
    SearingBlaze,
    Skullcrack,
    MonasterySwiftspear,
  }

  public class CardRegistry {
    public static Dictionary<Card, AbstractCard> Cards = new Dictionary<Card, AbstractCard> {
      {Card.Mountain, new MountainCard()},
      {Card.StompingGround, new StompingGroundCard() },
      {Card.SacredFoundry, new SacredFoundryCard() },
      {Card.AridMesa, new AridMesaCard()},
      {Card.WoodedFoothills, new WoodedFoothillsCard()},
      {Card.RiftBolt, new RiftBoltCard()},
      {Card.SearingBlaze, new SearingBlazeCard()},
      {Card.Skullcrack, new SkullcrackCard()},
      {Card.MonasterySwiftspear, new MonasterySwiftspearCard()},
    };
  }

  public class BasicLandTypesRegistry {
    public static Dictionary<BasicLandType, HashSet<Card>> BasicLandTypeCards =
      new Dictionary<BasicLandType, HashSet<Card>> {
        {BasicLandType.Plains, new HashSet<Card> {
          Card.SacredFoundry
        }},
        {BasicLandType.Island, new HashSet<Card> {}},
        {BasicLandType.Swamp, new HashSet<Card> {}},
        {BasicLandType.Mountain, new HashSet<Card> {
          Card.Mountain,
          Card.SacredFoundry,
          Card.StompingGround
        }},
        {BasicLandType.Forest, new HashSet<Card> {
          Card.StompingGround
        }}
      };
  }

  public class BasicLandColorsRegistry {
    public static Dictionary<BasicLandType, Color> BasicLandColors =
      new Dictionary<BasicLandType, Color> {
        {BasicLandType.Plains, Color.White},
        {BasicLandType.Island, Color.Blue},
        {BasicLandType.Swamp, Color.Black},
        {BasicLandType.Mountain, Color.Red},
        {BasicLandType.Forest, Color.Green}
      };
  }

  public class Program {
    public static void Main() {
      System.Console.ReadLine();
    }
  }

  public class GameStates {
    public static bool CouldPlaySorcery(GameState gameState) {
      return gameState.GameStatus == GameStatus.FirstMain ||
             gameState.GameStatus == GameStatus.SecondMain;
    }

    public static bool CouldPlayLand(GameState gameState) {
      return CouldPlaySorcery(gameState) &&
          gameState.LandPlaysThisTurn < gameState.MaxLandPlaysPerTurn;
    }

    public static bool CouldDeclareAttacks(GameState gameState) {
      return gameState.GameStatus == GameStatus.DeclareAttackers;
    }

    public static bool CouldDeclareBlocks(GameState gameState) {
      return gameState.GameStatus == GameStatus.OpponentDeclareBlockers;
    }

    public static bool ManaAvailable(GameState gameState, ManaValue manaValue) {
      return true;
    }
  }

  public abstract class AbstractCard {
    public abstract Card GetCard();

    public int GetCardId() {
      return (int)GetCard();
    }

    public abstract ManaValue GetPrintedManaCost();

    public virtual PowerAndToughness GetPrintedPowerAndToughness() {
      return new PowerAndToughness { Absent = true };
    }

    public virtual HashSet<CardSupertype> GetCardSupertypes() {
      return new HashSet<CardSupertype>();
    }
    public abstract HashSet<CardType> GetCardTypes();

    public virtual HashSet<CardSubtype> GetCardSubtypes() {
      return new HashSet<CardSubtype>();
    }

    public abstract void PopulateHandActions(GameState gameState, ICollection<Action> actions);

    public virtual void PopulatePermanentActions(GameState gameState, Permanent permanent,
      ICollection<Action> actions) { }

    public virtual void PerformPermanentAction(GameState gameState, Action action,
      Permanent permanent) { }

    public abstract void PerformHandAction(GameState gameState, Action action, int handIndex);
    public abstract void UndoAction(GameState gameState, Action action);

    public void RemoveFromHand(GameState gameState, int handIndex) {
      gameState.Hand.RemoveAt(handIndex);
    }
  }

  public abstract class AbstractLand : AbstractCard {
    public override void PopulateHandActions(GameState gameState, ICollection<Action> actions) {
      if (GameStates.CouldPlayLand(gameState)) {
        actions.Add(new Action { ActionType = ActionType.PlayLand, Source = GetCardId() });
      }
    }

    public override ManaValue GetPrintedManaCost() {
      return new ManaValue { Absent = true };
    }

    public override HashSet<CardType> GetCardTypes() {
      return new HashSet<CardType> { CardType.Land };
    }

    public override void PerformHandAction(GameState gameState, Action action, int handIndex) {

    }

    public override void UndoAction(GameState gameState, Action action) {

    }
  }

  public abstract class AbstractInstant : AbstractCard {
    public override void PopulateHandActions(GameState gameState, ICollection<Action> actions) {
      if (GameStates.ManaAvailable(gameState, GetPrintedManaCost())) {
        actions.Add(new Action { ActionType = ActionType.CastSpell, Source = GetCardId() });
      }
    }

    public override HashSet<CardType> GetCardTypes() {
      return new HashSet<CardType> { CardType.Instant };
    }
  }

  public abstract class AbstractSorcery : AbstractCard {
    public override void PopulateHandActions(GameState gameState, ICollection<Action> actions) {
      if (GameStates.CouldPlaySorcery(gameState) &&
          GameStates.ManaAvailable(gameState, GetPrintedManaCost())) {
        actions.Add(new Action { ActionType = ActionType.CastSpell, Source = GetCardId() });
      }
    }

    public override HashSet<CardType> GetCardTypes() {
      return new HashSet<CardType> { CardType.Sorcery };
    }
  }

  public abstract class AbstractCreature : AbstractCard {
    public abstract override PowerAndToughness GetPrintedPowerAndToughness();
    public abstract override HashSet<CardSubtype> GetCardSubtypes();

    public override void PopulateHandActions(GameState gameState, ICollection<Action> actions) {
      if (GameStates.CouldPlaySorcery(gameState) &&
          GameStates.ManaAvailable(gameState, GetPrintedManaCost())) {
        actions.Add(new Action { ActionType = ActionType.CastSpell, Source = GetCardId() });
      }
    }

    public override HashSet<CardType> GetCardTypes() {
      return new HashSet<CardType> { CardType.Creature };
    }

    public override void PopulatePermanentActions(GameState gameState, Permanent permanent,
      ICollection<Action> actions) {
      if (GameStates.CouldDeclareAttacks(gameState) && !permanent.Tapped &&
          CanAttack(gameState, permanent)) {
        actions.Add(new Action { ActionType = ActionType.DeclareAsAttacker, Source = permanent.Id });
      }

      if (GameStates.CouldDeclareBlocks(gameState) && !permanent.Tapped) {
        actions.Add(new Action { ActionType = ActionType.DeclareAsBlocker, Source = permanent.Id });
      }
    }

    protected virtual bool CanAttack(GameState gameState, Permanent permanent) {
      return permanent.ControlledSinceStartOfTurn;
    }
  }

  public abstract class AbstractBasicLand : AbstractLand {
    private readonly BasicLandType _type;

    protected AbstractBasicLand(BasicLandType type) {
      _type = type;
    }

    public override HashSet<CardSupertype> GetCardSupertypes() {
      return new HashSet<CardSupertype> { CardSupertype.Basic };
    }

    public override void PopulatePermanentActions(GameState gameState, Permanent permanent,
      ICollection<Action> actions) {
      if (permanent.Tapped) return;
      actions.Add(new Action {
        Source = permanent.Id,
        ActionType = ActionType.TapLandForMana
      });
    }
  }

  public abstract class AbstractFetchland : AbstractLand {
    private readonly BasicLandType _first;
    private readonly BasicLandType _second;

    protected AbstractFetchland(BasicLandType first, BasicLandType second) {
      _first = first;
      _second = second;
    }

    public override void PopulatePermanentActions(GameState gameState, Permanent permanent,
        ICollection<Action> actions) {
      if (permanent.Tapped || gameState.LifeTotal < 2) return;
      var fetchableCards = BasicLandTypesRegistry.BasicLandTypeCards[_first].Union(
          BasicLandTypesRegistry.BasicLandTypeCards[_second]);
      foreach (var card in fetchableCards) {
        if (gameState.Decklist.Contains(card)) {
          actions.Add(new Action {
            Source = GetCardId(),
            ActionType = ActionType.ActivateAbility,
            ActionChoices = card
          });
        }
      }
    }

    public override void UndoAction(GameState gameState, Action action) {
      throw new System.NotImplementedException();
    }
  }

  public abstract class AbstractShockLand : AbstractLand {
    private readonly BasicLandType _first;
    private readonly BasicLandType _second;

    private enum Choice {
      Shock,
      DontShock
    }

    protected AbstractShockLand(BasicLandType first, BasicLandType second) {
      _first = first;
      _second = second;
    }

    public override void PopulateHandActions(GameState gameState, ICollection<Action> actions) {
      if (!GameStates.CouldPlayLand(gameState)) return;
      actions.Add(new Action {
        ActionType = ActionType.PlayLand,
        Source = GetCardId(),
        ActionChoices = Choice.Shock
      });
      actions.Add(new Action {
        ActionType = ActionType.PlayLand,
        Source = GetCardId(),
        ActionChoices = Choice.DontShock
      });
    }

    public override void PopulatePermanentActions(GameState gameState, Permanent permanent,
        ICollection<Action> actions) {
      if (permanent.Tapped) return;
      actions.Add(new Action {
        ActionType = ActionType.TapLandForMana,
        Source = permanent.Id,
        ActionChoices = _first
      });
      actions.Add(new Action {
        ActionType = ActionType.TapLandForMana,
        Source = permanent.Id,
        ActionChoices = _second
      });
    }
  }

  public class MountainCard : AbstractBasicLand {
    public MountainCard() : base(BasicLandType.Mountain) { }

    public override Card GetCard() {
      return Card.Mountain;
    }
  }

  public class StompingGroundCard : AbstractShockLand {
    public StompingGroundCard() : base(BasicLandType.Forest, BasicLandType.Mountain) { }

    public override Card GetCard() {
      return Card.StompingGround;
    }
  }

  public class SacredFoundryCard : AbstractShockLand {
    public SacredFoundryCard() : base(BasicLandType.Forest, BasicLandType.Mountain) { }

    public override Card GetCard() {
      return Card.SacredFoundry;
    }
  }

  public class AridMesaCard : AbstractFetchland {
    public AridMesaCard() : base(BasicLandType.Plains, BasicLandType.Mountain) { }

    public override Card GetCard() {
      return Card.AridMesa;
    }
  }

  public class WoodedFoothillsCard : AbstractFetchland {
    public WoodedFoothillsCard() : base(BasicLandType.Mountain, BasicLandType.Forest) { }

    public override Card GetCard() {
      return Card.WoodedFoothills;
    }
  }

  public class RiftBoltCard : AbstractSorcery {
    public override Card GetCard() {
      return Card.RiftBolt;
    }

    public override ManaValue GetPrintedManaCost() {
      return new ManaValue { RedValue = 1, GenericValue = 2 };
    }

    public ManaValue GetSuspendCost() {
      return new ManaValue { RedValue = 1 };
    }

    public override void PopulateHandActions(GameState gameState, ICollection<Action> actions) {
      base.PopulateHandActions(gameState, actions);
      if (GameStates.ManaAvailable(gameState, GetSuspendCost())) {
        actions.Add(new Action { Source = GetCardId(), ActionType = ActionType.ActivateAbility });
      }
    }

    public override void PerformHandAction(GameState gameState, Action action, int handIndex) {
      throw new System.NotImplementedException();
    }

    public override void UndoAction(GameState gameState, Action action) {
      throw new System.NotImplementedException();
    }
  }

  public class SearingBlazeCard : AbstractInstant {
    public override Card GetCard() {
      return Card.SearingBlaze;
    }

    public override ManaValue GetPrintedManaCost() {
      return new ManaValue { RedValue = 2 };
    }

    public override void PerformHandAction(GameState gameState, Action action, int handIndex) {
      throw new System.NotImplementedException();
    }

    public override void UndoAction(GameState gameState, Action action) {
      throw new System.NotImplementedException();
    }
  }

  public class SkullcrackCard : AbstractInstant {
    public override Card GetCard() {
      return Card.Skullcrack;
    }

    public override ManaValue GetPrintedManaCost() {
      return new ManaValue { RedValue = 1, GenericValue = 1 };
    }

    public override void PerformHandAction(GameState gameState, Action action, int handIndex) {
      throw new System.NotImplementedException();
    }

    public override void UndoAction(GameState gameState, Action action) {
      throw new System.NotImplementedException();
    }
  }

  public class MonasterySwiftspearCard : AbstractCreature {
    public override Card GetCard() {
      return Card.MonasterySwiftspear;
    }

    public override ManaValue GetPrintedManaCost() {
      return new ManaValue { RedValue = 1 };
    }

    public override HashSet<CardSubtype> GetCardSubtypes() {
      return new HashSet<CardSubtype> { CardSubtype.Human, CardSubtype.Monk };
    }

    public override PowerAndToughness GetPrintedPowerAndToughness() {
      return new PowerAndToughness { Power = 1, Toughness = 2 };
    }

    protected override bool CanAttack(GameState gameState, Permanent permanent) {
      return true;
    }

    public override void PerformHandAction(GameState gameState, Action action, int handIndex) {
      throw new System.NotImplementedException();
    }

    public override void UndoAction(GameState gameState, Action action) {
      throw new System.NotImplementedException();
    }
  }
}
