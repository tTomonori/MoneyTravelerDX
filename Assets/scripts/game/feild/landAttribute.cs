using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum LandAttribute {
    none, north, east, south, west, center, waterside, woods
}

static public class LandAttributeMethods {
    static public Sprite getSprite(this LandAttribute e) {
        switch (e) {
            case LandAttribute.none:
                return null;
            case LandAttribute.north:
                return Resources.Load<Sprite>("sprites/feild/mass/attribute/north");
            case LandAttribute.east:
                return Resources.Load<Sprite>("sprites/feild/mass/attribute/east");
            case LandAttribute.south:
                return Resources.Load<Sprite>("sprites/feild/mass/attribute/south");
            case LandAttribute.west:
                return Resources.Load<Sprite>("sprites/feild/mass/attribute/west");
            case LandAttribute.center:
                return Resources.Load<Sprite>("sprites/feild/mass/attribute/center");
            case LandAttribute.waterside:
                return Resources.Load<Sprite>("sprites/feild/mass/attribute/waterside");
            case LandAttribute.woods:
                return Resources.Load<Sprite>("sprites/feild/mass/attribute/woods");
        }
        throw new Exception();
    }
    static public string getName(this LandAttribute e) {
        switch (e) {
            case LandAttribute.none:
                return "";
            case LandAttribute.north:
                return "北部地方";
            case LandAttribute.east:
                return "東部地方";
            case LandAttribute.south:
                return "南部地方";
            case LandAttribute.west:
                return "西部地方";
            case LandAttribute.center:
                return "中部地方";
            case LandAttribute.waterside:
                return "水域";
            case LandAttribute.woods:
                return "緑地";
        }
        throw new Exception();
    }
}