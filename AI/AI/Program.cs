using System.Collections.Generic;
using System.Runtime.Hosting;

namespace AI {

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
    Green
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

  public struct ManaValue {
    public int GenericValue;
    public int WhiteValue;
    public int BlueValue;
    public int BlackValue;
    public int RedValue;
    public int GreenValue;
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
    PlayLand,
    CastSpell,
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
    public List<Permanent> Permanents;
    public int LandPlaysThisTurn;
    public int MaxLandPlaysPerTurn;
  }

  public struct Action {
    public int Source;
    public ActionType ActionType;
    public System.ValueType ActionChoices;
  }

  public class Game {
    GameState _gameState;
  }

  public class CardData {
    public static void Main() {
      System.Console.WriteLine("Hello, World!");
      System.Console.ReadLine();
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
    public abstract List<CardType> GetCardTypes();

    public abstract void PopulateHandActions(GameState gameState, ICollection<Action> actions);
    public virtual void PopulatePermanentActions(GameState gameState, Permanent permanent,
        ICollection<Action> actions) { }
    public virtual void PopulateGraveyardActions(GameState gameState, ICollection<Action> actions) { }
    public virtual void PopulateStackActions(GameState gameState, ICollection<Action> actions) { }

    public abstract void PerformAction(GameState gameState, Action action);
    public abstract void UndoAction(GameState gameState, Action action);
  }

  public abstract class AbstractLand : AbstractCard {
    public override void PopulateHandActions(GameState gameState, ICollection<Action> actions) {
      if (GameStates.CouldPlayLand(gameState)) {
        actions.Add(new Action { ActionType = ActionType.PlayLand, Source = GetCardId() });
      }
    }
  }

  public abstract class AbstractInstant : AbstractCard {
    public override void PopulateHandActions(GameState gameState, ICollection<Action> actions) {
      if (GameStates.ManaAvailable(gameState, GetPrintedManaCost())) {
        actions.Add(new Action { ActionType = ActionType.CastSpell, Source = GetCardId() });
      }
    }
  }

  public abstract class AbstractSorcery : AbstractCard {
    public override void PopulateHandActions(GameState gameState, ICollection<Action> actions) {
      if (GameStates.CouldPlaySorcery(gameState) &&
          GameStates.ManaAvailable(gameState, GetPrintedManaCost())) {
        actions.Add(new Action { ActionType = ActionType.CastSpell, Source = GetCardId() });
      }
    }
  }

  public abstract class AbstractCreature : AbstractCard {
    public override void PopulateHandActions(GameState gameState, ICollection<Action> actions) {
      if (GameStates.CouldPlaySorcery(gameState) &&
          GameStates.ManaAvailable(gameState, GetPrintedManaCost())) {
        actions.Add(new Action { ActionType = ActionType.CastSpell, Source = GetCardId() });
      }
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

  public abstract class AbstractFetchland : AbstractLand {
    private BasicLandType _first;
    private BasicLandType _second;

    protected AbstractFetchland(BasicLandType first, BasicLandType second) {
      _first = first;
      _second = second;
    }

    public override ManaValue GetPrintedManaCost() {
      return new ManaValue();
    }

    public override List<CardType> GetCardTypes() {
      return new List<CardType> { CardType.Land };
    }

    public override void PopulatePermanentActions(GameState gameState, Permanent permanent,
        ICollection<Action> actions) {
      if (permanent.Tapped) return;
      actions.Add(new Action {
        Source = GetCardId(),
        ActionType = ActionType.ActivateAbility,
        ActionChoices = _first
      });
      actions.Add(new Action {
        Source = GetCardId(),
        ActionType = ActionType.ActivateAbility,
        ActionChoices = _second
      });
    }

    public override void PerformAction(GameState gameState, Action action) {
      throw new System.NotImplementedException();
    }

    public override void UndoAction(GameState gameState, Action action) {
      throw new System.NotImplementedException();
    }
  }

  public class AridMesaCard : AbstractFetchland {
    public AridMesaCard() : base(BasicLandType.Plains, BasicLandType.Mountain) {
    }

    public override Card GetCard() {
      return Card.AridMesa;
    }
  }

  public class WoodedFoothillsCard : AbstractFetchland {
    public WoodedFoothillsCard() : base(BasicLandType.Mountain, BasicLandType.Forest) {
    }

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

    public override List<CardType> GetCardTypes() {
      return new List<CardType> { CardType.Sorcery };
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

    public override void PerformAction(GameState gameState, Action action) {
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

    public override List<CardType> GetCardTypes() {
      return new List<CardType> { CardType.Instant };
    }

    public override void PerformAction(GameState gameState, Action action) {
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

    public override List<CardType> GetCardTypes() {
      return new List<CardType> { CardType.Instant };
    }

    public override void PerformAction(GameState gameState, Action action) {
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

    public override List<CardType> GetCardTypes() {
      return new List<CardType> { CardType.Creature };
    }

    protected override bool CanAttack(GameState gameState, Permanent permanent) {
      return true;
    }

    public override void PerformAction(GameState gameState, Action action) {
      throw new System.NotImplementedException();
    }

    public override void UndoAction(GameState gameState, Action action) {
      throw new System.NotImplementedException();
    }
  }
}
