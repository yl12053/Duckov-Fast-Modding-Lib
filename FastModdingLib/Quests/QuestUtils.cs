using Duckov.Quests;
using Duckov.Quests.Relations;
using Duckov.Utilities;
using System.Collections.Generic;
using UnityEngine;

namespace FastModdingLib
{
    public static class QuestUtils
    {
        public static Dictionary<Quest, string> addedQuests = new Dictionary<Quest, string>();
        public static void RegisterQuest(QuestData data, string modid = "FastModdingLib")
        {
            Quest quest = new GameObject($"Quest_{data.displayName}").AddComponent<Quest>();
            UnityEngine.Object.DontDestroyOnLoad(quest.gameObject);
            quest.gameObject.SetActive(false);
            quest.DisplayNameRaw = data.displayName;
            quest.DescriptionRaw = data.description;
            quest.ID = data.ID;
            quest.QuestGiverID = data.questGiver;
            quest.requireLevel = data.requireLevel;
            
            if(data.requireScene != null && data.requireScene != "")
            {
                quest.requireSceneID = data.requireScene;
            }

            if (data.requireItemID != -1)
            {
                quest.requiredItemID = data.requireItemID;
                quest.requiredItemCount = 1;
            }
            
            foreach (var taskData in data.tasks)
            {
                Task task = taskData.SetTask(quest);
                if (task != null)
                {
                    quest.tasks.Add(task);
                }
            }

            foreach (var rewardData in data.rewards)
            {
                Reward reward = rewardData.SetReward(quest);
                if (reward != null)
                {
                    quest.rewards.Add(reward);
                }
            }
            GameplayDataSettings.QuestCollection.Add(quest);
            addedQuests.Add(quest, modid);
            Debug.Log($"[FML] Registered quest: {data.displayName} (ID: {data.ID}) from mod: {modid}");
            
        }

        public static void UnregisterQuest(int ID)
        {
            foreach (var item in addedQuests)
            {
                if (item.Key.id == ID)
                {
                    GameplayDataSettings.QuestCollection.Remove(item.Key);
                    UnityEngine.Object.Destroy(item.Key.gameObject);
                    GameplayDataSettings.QuestRelation.RemoveNode(GameplayDataSettings.QuestRelation.GetNode(ID));
                    addedQuests.Remove(item.Key);
                    Debug.Log($"Unregistered custom quest: {item.Key.id}");
                    break;
                }
            }
        }

        public static void UnregisterQuestAll(string modID)
        {
            foreach (var item in addedQuests)
            {
                if (item.Value.Equals(modID))
                {
                    GameplayDataSettings.QuestCollection.Remove(item.Key);
                    UnityEngine.Object.Destroy(item.Key.gameObject);
                    GameplayDataSettings.QuestRelation.RemoveNode(GameplayDataSettings.QuestRelation.GetNode(item.Key.ID));
                    addedQuests.Remove(item.Key);
                    Debug.Log($"Unregistered custom quest: {item.Key.id}");
                }
            }
        }

        public static void AddQuestRelation(int id, int before = -1, int after = -1)
        {
            QuestRelationNode item = new QuestRelationNode
            {
                questID = id
            };
            if(before != -1)
            {
                QuestRelationNode source = new QuestRelationNode
                {
                    questID = before
                };
                item.inConnections.Add(QuestRelationConnection.Create(source, item));
            }

            if(after != -1)
            {
                QuestRelationNode target = new QuestRelationNode
                {
                    questID = after
                };
                item.outConnections.Add(QuestRelationConnection.Create(item, target));
            }

            GameplayDataSettings.QuestRelation.allNodes.Add(item);
        }

    }
}