﻿using System.Collections.Generic;

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
    DeclareBlockers,
    SecondMain,
    OpponentUpkeep,
    OpponentBeginCombat,
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
    TapLand,
    ActivateAbility,
    DeclareAsAttacker,
    DeclareAsBlocker
  }

  public struct CardState {

  }

  public struct GameState {
    public Turn Turn;
    public GameStatus GameStatus;
    public List<Card> Hand;
    public ManaValue ManaPool;
    public List<Card> Permanents;
    public int LandPlaysThisTurn;
    public int MaxLandPlaysPerTurn;
  }

  public struct Action {
    public Card Card;
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

  public abstract class AbstractCard {
    public abstract Card GetCard();
    public abstract ManaValue GetPrintedManaCost();
    public abstract List<CardType> GetCardTypes();
    public abstract void PopulateActions(GameState gameState, ICollection<Action> actions, Zone zone);
    public abstract void PerformAction(GameState gameState, Action action);
    public abstract void UndoAction(GameState gameState, Action action);
  }

  public abstract class AbstractLand : AbstractCard {
    public override void PopulateActions(GameState gameState, ICollection<Action> actions, Zone zone) {
      switch (zone) {
        case Zone.Hand:
          if ((gameState.GameStatus == GameStatus.FirstMain ||
              gameState.GameStatus == GameStatus.SecondMain) &&
              gameState.LandPlaysThisTurn < gameState.MaxLandPlaysPerTurn) {
            actions.Add(new Action { ActionType = ActionType.PlayLand, Card = GetCard() });
          }
          return;
      }
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

    public override void PopulateActions(GameState gameState, ICollection<Action> actions, Zone zone) {
      base.PopulateActions(gameState, actions, zone);
      switch (zone) {
        case Zone.Battlefield:

      }
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

  public class RiftBoltCard : AbstractCard {
    public override Card GetCard() {
      return Card.RiftBolt;
    }

    public override ManaValue GetPrintedManaCost() {
      return new ManaValue { RedValue = 1, GenericValue = 2 };
    }

    public override List<CardType> GetCardTypes() {
      return new List<CardType> { CardType.Sorcery };
    }

    public override void PopulateActions(GameState gameState, ICollection<Action> actions, Zone zone) {
      throw new System.NotImplementedException();
    }

    public override void PerformAction(GameState gameState, Action action) {
      throw new System.NotImplementedException();
    }

    public override void UndoAction(GameState gameState, Action action) {
      throw new System.NotImplementedException();
    }
  }

  public class SearingBlazeCard : AbstractCard {
    public override Card GetCard() {
      return Card.SearingBlaze;
    }

    public override ManaValue GetPrintedManaCost() {
      return new ManaValue { RedValue = 2 };
    }

    public override List<CardType> GetCardTypes() {
      return new List<CardType> { CardType.Instant };
    }

    public override void PopulateActions(GameState gameState, ICollection<Action> actions, Zone zone) {
      throw new System.NotImplementedException();
    }

    public override void PerformAction(GameState gameState, Action action) {
      throw new System.NotImplementedException();
    }

    public override void UndoAction(GameState gameState, Action action) {
      throw new System.NotImplementedException();
    }
  }

  public class SkullcrackCard : AbstractCard {
    public override Card GetCard() {
      return Card.Skullcrack;
    }

    public override ManaValue GetPrintedManaCost() {
      return new ManaValue { RedValue = 1, GenericValue = 1 };
    }

    public override List<CardType> GetCardTypes() {
      return new List<CardType> { CardType.Instant };
    }

    public override void PopulateActions(GameState gameState, ICollection<Action> actions, Zone zone) {
      throw new System.NotImplementedException();
    }

    public override void PerformAction(GameState gameState, Action action) {
      throw new System.NotImplementedException();
    }

    public override void UndoAction(GameState gameState, Action action) {
      throw new System.NotImplementedException();
    }
  }

  public class MonasterySwiftspearCard : AbstractCard {
    public override Card GetCard() {
      return Card.MonasterySwiftspear;
    }

    public override ManaValue GetPrintedManaCost() {
      return new ManaValue { RedValue = 1 };
    }

    public override List<CardType> GetCardTypes() {
      return new List<CardType> { CardType.Creature };
    }

    public override void PopulateActions(GameState gameState, ICollection<Action> actions, Zone zone) {
      throw new System.NotImplementedException();
    }

    public override void PerformAction(GameState gameState, Action action) {
      throw new System.NotImplementedException();
    }

    public override void UndoAction(GameState gameState, Action action) {
      throw new System.NotImplementedException();
    }
  }
}
