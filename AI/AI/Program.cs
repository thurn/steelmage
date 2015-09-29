using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;

namespace AI {
  public static class Empty {
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
    Choice,
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

  public enum TriggerType {
    BeginningOfUpkeep,
    EnterBattlefield,
    CastSpell,
    CreatureAttacks,
  }


  public struct StackItem {
    public int Id;
    public Card Source;
  }

  public class Library {
    public void RemoveCard(Card card) {

    }

    public bool ContainsCard(Card card) {
      return true;
    }

    public Card GetRandomCard() {
      return Card.WoodedFoothills;
    }
  }

  public class GameState {
    public Turn Turn;
    public GameStatus GameStatus;
    public Dictionary<int, Card> Hand;
    public ManaValue ManaPool;
    public Dictionary<int, Permanent> Permanents;
    public HashSet<int> AttackingCreatures;
    public Dictionary<int, int> BlockingCreatures;
    public List<StackItem> Stack;
    public Library Library;
    public byte LibrarySize;
    public byte LifeTotal;
    public byte OpponentLifeTotal;
    public byte LandPlaysThisTurn;
    public byte MaxLandPlaysPerTurn;
    public int NextId;
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

  public struct UndoZoneChange {
    public int SourceId;
    public int DestinationId;
    public Card Card;
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

    public static void AddManaOfColorToPool(GameState gameState, Color color, byte quantity) {
      AddToManaPool(gameState, new ManaValue {
        WhiteValue = color == Color.White ? quantity : (byte)0,
        BlueValue = color == Color.Blue ? quantity : (byte)0,
        BlackValue = color == Color.Black ? quantity : (byte)0,
        RedValue = color == Color.Red ? quantity : (byte)0,
        GreenValue = color == Color.Green ? quantity : (byte)0,
        GenericValue = color == Color.Colorless ? quantity : (byte)0
      });
    }

    public static void AddToManaPool(GameState gameState, ManaValue value) {
      gameState.ManaPool = new ManaValue {
        WhiteValue = (byte)(gameState.ManaPool.WhiteValue + value.WhiteValue),
        BlueValue = (byte)(gameState.ManaPool.BlueValue + value.BlueValue),
        BlackValue = (byte)(gameState.ManaPool.BlackValue + value.BlackValue),
        RedValue = (byte)(gameState.ManaPool.RedValue + value.RedValue),
        GreenValue = (byte)(gameState.ManaPool.GreenValue + value.GreenValue),
        GenericValue = (byte)(gameState.ManaPool.WhiteValue + value.GenericValue),
      };
    }

    public static bool ManaAvailable(GameState gameState, ManaValue manaValue,
        out ManaValue result) {
      result = new ManaValue { Absent = true };
      var pool = gameState.ManaPool;
      var genericToPay = manaValue.GenericValue - pool.GenericValue;
      result.GenericValue = (byte)Math.Max(0, pool.GenericValue - manaValue.GenericValue);

      var floating = pool.WhiteValue - manaValue.WhiteValue;
      if (floating < 0) return false;
      if (genericToPay > 0) {
        var genericPaid = Math.Min(genericToPay, floating);
        genericToPay -= genericPaid;
        floating -= genericPaid;
      }
      result.WhiteValue = (byte)floating;

      floating = pool.BlueValue - manaValue.BlueValue;
      if (floating < 0) return false;
      if (genericToPay > 0) {
        var genericPaid = Math.Min(genericToPay, floating);
        genericToPay -= genericPaid;
        floating -= genericPaid;
      }
      result.BlueValue = (byte)floating;

      floating = pool.BlackValue - manaValue.BlackValue;
      if (floating < 0) return false;
      if (genericToPay > 0) {
        var genericPaid = Math.Min(genericToPay, floating);
        genericToPay -= genericPaid;
        floating -= genericPaid;
      }
      result.BlackValue = (byte)floating;

      floating = pool.RedValue - manaValue.RedValue;
      if (floating < 0) return false;
      if (genericToPay > 0) {
        var genericPaid = Math.Min(genericToPay, floating);
        genericToPay -= genericPaid;
        floating -= genericPaid;
      }
      result.RedValue = (byte)floating;

      floating = pool.GreenValue - manaValue.GreenValue;
      if (floating < 0) return false;
      if (genericToPay > 0) {
        var genericPaid = Math.Min(genericToPay, floating);
        genericToPay -= genericPaid;
        floating -= genericPaid;
      }
      result.GreenValue = (byte)floating;

      if (genericToPay > 0) return false;
      result.Absent = false;
      return true;
    }

    public static bool ManaAvailable(GameState gameState, ManaValue manaValue) {
      ManaValue result;
      return ManaAvailable(gameState, manaValue, out result);
    }

    public static int CreatePermanent(GameState gameState, Card card) {
      var permanent = new Permanent {
        Id = gameState.NextId++,
        Card = card,
        ControlledSinceStartOfTurn = false,
        Tapped = false
      };
      gameState.Permanents.Add(permanent.Id, permanent);
      return permanent.Id;
    }

    public static int CreateStackItem(GameState gameState, Card source) {
      var stackItem = new StackItem {
        Id = gameState.NextId++,
        Source = source
      };
      gameState.Stack.Add(stackItem);
      return stackItem.Id;
    }

    public static int AddToHand(GameState gameState, Card card) {
      var id = gameState.NextId++;
      gameState.Hand.Add(id, card);
      return id;
    }

    public static UndoZoneChange MoveFromHandToBattlefield(GameState gameState, int handId) {
      var card = gameState.Hand[handId];
      gameState.Hand.Remove(handId);
      var permanentId = CreatePermanent(gameState, card);
      return new UndoZoneChange { Card = card, SourceId = handId, DestinationId = permanentId };
    }

    public static UndoZoneChange MoveFromBattlefieldToHand(GameState gameState, int permanentId) {
      var permanent = gameState.Permanents[permanentId];
      gameState.Permanents.Remove(permanentId);
      var handId = AddToHand(gameState, permanent.Card);
      return new UndoZoneChange {
        Card = permanent.Card,
        SourceId = permanent.Id,
        DestinationId = handId
      };
    }

    public static UndoZoneChange MoveFromHandToStack(GameState gameState, int handId) {
      var card = gameState.Hand[handId];
      gameState.Hand.Remove(handId);
      var stackId = CreateStackItem(gameState, card);
      return new UndoZoneChange { Card = card, SourceId = handId, DestinationId = stackId };
    }

    public static UndoZoneChange MoveFromTopOfStackToHand(GameState gameState) {
      var stackItem = gameState.Stack[gameState.Stack.Count - 1];
      gameState.Stack.RemoveAt(gameState.Stack.Count - 1);
      var newId = gameState.NextId++;
      gameState.Hand.Add(newId, stackItem.Source);
      return new UndoZoneChange {
        Card = stackItem.Source,
        SourceId = stackItem.Id,
        DestinationId = newId
      };
    }
  }

  public abstract class AbstractCard {
    public abstract Card GetCard();

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

    public abstract void PopulateHandActions(GameState gameState, ICollection<Action> actions,
        int handId);
    public abstract ValueType PerformHandAction(GameState gameState, Action action, int handId);
    public abstract void UndoHandAction(GameState gameState, Action action, ValueType undoState);

    public virtual void PopulatePermanentActions(GameState gameState, Permanent permanent,
        ICollection<Action> actions) { }
    public virtual ValueType PerformPermanentAction(GameState gameState, Action action,
      Permanent permanent) {
      return Empty.Value;
    }
    public virtual void UndoPermanentAction(GameState gameState, Action action, int permanentId,
        ValueType undoState) { }

    public virtual void PopulateTriggerActions(GameState gameState, ICollection<Action> actions,
        TriggerType triggerType, int sourceId) { }
    public virtual ValueType PerformTriggerAction(GameState gameState, Action action,
        int sourceId) {
      return Empty.Value;
    }
    public virtual void UndoTriggerAction(GameState gameState, Action action, int sourceId) { }

    public abstract ValueType Resolve(GameState gameState);
    public abstract void Unresolve(GameState gameState, ValueType undoState);
  }

  public abstract class AbstractLand : AbstractCard {
    public override void PopulateHandActions(GameState gameState, ICollection<Action> actions,
        int handId) {
      if (GameStates.CouldPlayLand(gameState)) {
        actions.Add(new Action { ActionType = ActionType.PlayLand, Source = handId });
      }
    }

    public override ManaValue GetPrintedManaCost() {
      return new ManaValue { Absent = true };
    }

    public override HashSet<CardType> GetCardTypes() {
      return new HashSet<CardType> { CardType.Land };
    }

    public override ValueType PerformHandAction(GameState gameState, Action action,
        int handIndex) {
      return GameStates.MoveFromHandToBattlefield(gameState, handIndex);
    }

    public override void UndoHandAction(GameState gameState, Action action, ValueType undoState) {
      var undoZoneChange = (UndoZoneChange)undoState;
      GameStates.MoveFromBattlefieldToHand(gameState, undoZoneChange.DestinationId);
    }

    public override ValueType Resolve(GameState gameState) {
      return Empty.Value;
    }

    public override void Unresolve(GameState gameState, ValueType undoState) { }
  }

  public abstract class AbstractSpell : AbstractCard {
    public override void PopulateHandActions(GameState gameState, ICollection<Action> actions,
        int handId) {
      if (GameStates.ManaAvailable(gameState, GetPrintedManaCost()) && CanCast(gameState)) {
        actions.Add(new Action { ActionType = ActionType.CastSpell, Source = handId });
      }
    }

    public override ValueType PerformHandAction(GameState gameState, Action action,
        int handId) {
      if (action.ActionType == ActionType.CastSpell && action.ActionChoices.Equals(Empty.Value)) {
        var oldManaPool = gameState.ManaPool;
        ManaValue newManaPool;
        GameStates.ManaAvailable(gameState, GetPrintedManaCost(), out newManaPool);
        gameState.ManaPool = newManaPool;
        GameStates.MoveFromHandToStack(gameState, handId);
        return oldManaPool;
      }
      return Empty.Value;
    }

    public override void UndoHandAction(GameState gameState, Action action, ValueType undoState) {
      if (action.ActionType == ActionType.CastSpell && action.ActionChoices.Equals(Empty.Value)) {
        gameState.ManaPool = (ManaValue)undoState;
        GameStates.MoveFromTopOfStackToHand(gameState);
      }
    }

    protected abstract bool CanCast(GameState gameState);
  }

  public abstract class AbstractInstant : AbstractSpell {
    protected override bool CanCast(GameState gameState) {
      return true;
    }

    public override HashSet<CardType> GetCardTypes() {
      return new HashSet<CardType> { CardType.Instant };
    }
  }

  public abstract class AbstractSorcery : AbstractSpell {
    protected override bool CanCast(GameState gameState) {
      return GameStates.CouldPlaySorcery(gameState);
    }

    public override HashSet<CardType> GetCardTypes() {
      return new HashSet<CardType> { CardType.Sorcery };
    }
  }

  public abstract class AbstractCreature : AbstractSpell {
    public abstract override PowerAndToughness GetPrintedPowerAndToughness();
    public abstract override HashSet<CardSubtype> GetCardSubtypes();

    protected override bool CanCast(GameState gameState) {
      return GameStates.CouldPlaySorcery(gameState);
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
        foreach (var attacker in gameState.AttackingCreatures) {
          actions.Add(new Action {
            ActionType = ActionType.DeclareAsBlocker,
            Source = permanent.Id,
            ActionChoices = attacker
          });
        }
      }
    }

    public override ValueType PerformPermanentAction(GameState gameState, Action action,
      Permanent permanent) {
      switch (action.ActionType) {
        case ActionType.DeclareAsAttacker:
          gameState.AttackingCreatures.Add(permanent.Id);
          break;
        case ActionType.DeclareAsBlocker:
          gameState.BlockingCreatures.Add(permanent.Id, (int)action.ActionChoices);
          break;
      }
      return Empty.Value;
    }

    public override void UndoPermanentAction(GameState gameState, Action action, int permanentId,
        ValueType undoState) {
      switch (action.ActionType) {
        case ActionType.DeclareAsAttacker:
          gameState.AttackingCreatures.Remove(permanentId);
          break;
        case ActionType.DeclareAsBlocker:
          gameState.BlockingCreatures.Remove(permanentId);
          break;
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

    public override ValueType PerformPermanentAction(GameState gameState, Action action,
      Permanent permanent) {
      if (action.ActionType == ActionType.TapLandForMana) {
        var oldManaPool = gameState.ManaPool;
        var color = BasicLandColorsRegistry.BasicLandColors[_type];
        GameStates.AddManaOfColorToPool(gameState, color, 1);
        return oldManaPool;
      }
      return Empty.Value;
    }

    public override void UndoPermanentAction(GameState gameState, Action action, int permanentId,
        ValueType undoState) {
      if (action.ActionType == ActionType.TapLandForMana) {
        gameState.ManaPool = (ManaValue)undoState;
      }
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
        if (gameState.Library.ContainsCard(card)) {
          actions.Add(new Action {
            Source = permanent.Id,
            ActionType = ActionType.ActivateAbility,
            ActionChoices = card
          });
        }
      }
    }

    public override ValueType PerformPermanentAction(GameState gameState, Action action,
      Permanent permanent) {
      gameState.LifeTotal--;
      var card = (Card)action.ActionChoices;
      gameState.Library.RemoveCard(card);
      GameStates.CreatePermanent(gameState, card);
      return Empty.Value;
    }

    public override void UndoPermanentAction(GameState gameState, Action action, int permanentId,
        ValueType undoState) {
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

    public override void PopulateTriggerActions(GameState gameState, ICollection<Action> actions,
      TriggerType triggerType, int sourceId) {
      if (triggerType != TriggerType.EnterBattlefield) return;
      actions.Add(new Action {
        ActionType = ActionType.Choice,
        Source = sourceId,
        ActionChoices = Choice.Shock
      });
      actions.Add(new Action {
        ActionType = ActionType.Choice,
        Source = sourceId,
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

    public override void PopulateHandActions(GameState gameState, ICollection<Action> actions,
        int handId) {
      base.PopulateHandActions(gameState, actions, handId);
      if (GameStates.ManaAvailable(gameState, GetSuspendCost())) {
        actions.Add(new Action { Source = handId, ActionType = ActionType.ActivateAbility });
      }
    }

    public override ValueType Resolve(GameState gameState) {
      throw new System.NotImplementedException();
    }

    public override void Unresolve(GameState gameState, ValueType undoState) {
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

    public override ValueType Resolve(GameState gameState) {
      throw new System.NotImplementedException();
    }

    public override void Unresolve(GameState gameState, ValueType undoState) {
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

    public override ValueType Resolve(GameState gameState) {
      throw new System.NotImplementedException();
    }

    public override void Unresolve(GameState gameState, ValueType undoState) {
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

    public override ValueType Resolve(GameState gameState) {
      throw new System.NotImplementedException();
    }

    public override void Unresolve(GameState gameState, ValueType undoState) {
      throw new System.NotImplementedException();
    }
  }
}
