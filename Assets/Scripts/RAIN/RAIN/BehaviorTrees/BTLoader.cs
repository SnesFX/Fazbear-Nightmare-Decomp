using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using RAIN.Action;
using RAIN.Animation;
using RAIN.BehaviorTrees.Actions;
using RAIN.Representation;
using RAIN.Serialization;
using UnityEngine;

namespace RAIN.BehaviorTrees
{
	public static class BTLoader
	{
		private static string TAG_BT = "behaviortree";

		private static string TAG_COMMENT = "#comment";

		private static string ATTR_DEBUGBREAK = "debugbreak";

		private static string ATTR_REPEATUNTIL = "repeatuntil";

		private static string ATTR_STARTINGPRIORITY = "startingpriority";

		private static string ATTR_RUNNINGPRIORITY = "runningpriority";

		private static string ATTR_PRIORITY = "priority";

		private static string ATTR_WEIGHT = "weight";

		public static BTNode Load(BTAsset asset, List<BTAssetBinding> bindingList, string rootName = null, Assembly customActionAssembly = null)
		{
			ObjectDocument objectDocument = ObjectDocument.CreateFieldDocument(FieldDocumentType.Xml);
			objectDocument.SetDocument(asset.GetTreeData());
			return Load(objectDocument, TrimBindingList(bindingList, asset), rootName, customActionAssembly);
		}

		public static BTNode Load(string aXml, List<BTAssetBinding> bindingList, string rootName = null, Assembly customActionAssembly = null)
		{
			ObjectDocument objectDocument = ObjectDocument.CreateFieldDocument(FieldDocumentType.Xml);
			objectDocument.SetDocument(aXml);
			return Load(objectDocument, bindingList, rootName, customActionAssembly);
		}

		public static BTNode Load(ObjectDocument tDocument, List<BTAssetBinding> bindingList, string rootName = null, Assembly customActionAssembly = null)
		{
			BTNode bTNode = null;
			if (rootName != null)
			{
				rootName = rootName.Trim();
				if (rootName.Length == 0)
				{
					rootName = null;
				}
			}
			ObjectElement objectElement = tDocument.DocumentElement;
			while (objectElement != null && objectElement.Name.ToLower() != TAG_BT)
			{
				objectElement = objectElement.NextSibling;
			}
			if (objectElement != null)
			{
				objectElement = objectElement.GetChild(0);
			}
			while (objectElement != null)
			{
				string text = objectElement.Name.ToLower();
				if (!(text == TAG_COMMENT) && bTNode == null)
				{
					string text2 = null;
					if (objectElement.HasAttribute("name"))
					{
						text2 = objectElement.GetAttribute("name");
					}
					if (rootName == null || rootName == text2)
					{
						bTNode = LoadNode(objectElement, bindingList, customActionAssembly);
					}
				}
				objectElement = objectElement.NextSibling;
			}
			return bTNode;
		}

		public static BTNode LoadNode(ObjectElement node, List<BTAssetBinding> bindings = null, Assembly customActionAssembly = null)
		{
			BTNode bTNode = null;
			try
			{
				if (node.Name == "action")
				{
					BTActionNode bTActionNode = new BTActionNode();
					bTNode = bTActionNode;
					if (node.HasAttribute("name"))
					{
						bTNode.ActionName = node.GetAttribute("name");
					}
					if (node.HasAttribute("classname"))
					{
						if (customActionAssembly != null)
						{
							bTActionNode.SetAction(customActionAssembly, node.GetAttribute("classname"));
						}
						else if (node.HasAttribute("namespace"))
						{
							bTActionNode.SetAction(node.GetAttribute("namespace"), node.GetAttribute("classname"));
						}
						if (bTActionNode.actionInstance != null)
						{
							try
							{
								if (node.HasAttribute("parameters") && node.HasAttribute("parametervalues"))
								{
									byte[] array = Convert.FromBase64String(node.GetAttribute("parameters"));
									string @string = Encoding.UTF8.GetString(array, 0, array.Length);
									byte[] array2 = Convert.FromBase64String(node.GetAttribute("parametervalues"));
									string string2 = Encoding.UTF8.GetString(array2, 0, array2.Length);
									char[] separator = new char[1];
									List<string> list = new List<string>(@string.Split(separator));
									separator = new char[1];
									List<string> list2 = new List<string>(string2.Split(separator));
									FieldInfo[] fields = bTActionNode.actionInstance.GetType().GetFields();
									for (int i = 0; i < list.Count; i++)
									{
										for (int j = 0; j < fields.Length; j++)
										{
											if (fields[j].FieldType == typeof(Expression) && fields[j].Name == list[i])
											{
												if (list2.Count > i)
												{
													Expression value = ExpressionParser.Parse(list2[i]);
													fields[j].SetValue(bTActionNode.actionInstance, value);
												}
												break;
											}
										}
									}
								}
							}
							catch (Exception ex)
							{
								string text = "";
								if (node.HasAttribute("name"))
								{
									text = "[" + node.GetAttribute("name") + "]";
								}
								Debug.LogError("Unable to load custom action node " + text + ": " + ex.Message);
							}
						}
					}
				}
				else if (node.Name == "decision")
				{
					if (node.HasAttribute("classname"))
					{
						if (customActionAssembly != null)
						{
							bTNode = RAINDecision.LoadDecisionInstance(customActionAssembly, node.GetAttribute("classname"));
						}
						else if (node.HasAttribute("namespace"))
						{
							bTNode = RAINDecision.LoadDecisionInstance(node.GetAttribute("namespace"), node.GetAttribute("classname"));
						}
						if (bTNode != null)
						{
							try
							{
								if (node.HasAttribute("parameters") && node.HasAttribute("parametervalues"))
								{
									byte[] array3 = Convert.FromBase64String(node.GetAttribute("parameters"));
									string string3 = Encoding.UTF8.GetString(array3, 0, array3.Length);
									byte[] array4 = Convert.FromBase64String(node.GetAttribute("parametervalues"));
									string string4 = Encoding.UTF8.GetString(array4, 0, array4.Length);
									char[] separator = new char[1];
									List<string> list3 = new List<string>(string3.Split(separator));
									separator = new char[1];
									List<string> list4 = new List<string>(string4.Split(separator));
									FieldInfo[] fields2 = bTNode.GetType().GetFields();
									for (int k = 0; k < list3.Count; k++)
									{
										for (int l = 0; l < fields2.Length; l++)
										{
											if (fields2[l].FieldType == typeof(Expression) && fields2[l].Name == list3[k])
											{
												if (list4.Count > k)
												{
													Expression value2 = ExpressionParser.Parse(list4[k]);
													fields2[l].SetValue(bTNode, value2);
												}
												break;
											}
										}
									}
								}
							}
							catch (Exception ex2)
							{
								string text2 = "";
								if (node.HasAttribute("name"))
								{
									text2 = "[" + node.GetAttribute("name") + "]";
								}
								Debug.LogError("Unable to load custom decision node " + text2 + ": " + ex2.Message);
							}
						}
					}
				}
				else if (node.Name == "expression")
				{
					BTExpressionNode bTExpressionNode = new BTExpressionNode();
					bTNode = bTExpressionNode;
					if (node.HasAttribute("expression"))
					{
						bTExpressionNode.EvaluatedExpression = ExpressionParser.Parse(node.GetAttribute("expression"));
					}
					else
					{
						bTExpressionNode.EvaluatedExpression = ExpressionParser.Parse("");
					}
					if (node.HasAttribute("returnvalue"))
					{
						if (node.GetAttribute("returnvalue") == "success")
						{
							bTExpressionNode.ReturnValue = BTExpressionNode.ReturnType.Success;
						}
						else if (node.GetAttribute("returnvalue") == "failure")
						{
							bTExpressionNode.ReturnValue = BTExpressionNode.ReturnType.Failure;
						}
						else if (node.GetAttribute("returnvalue") == "evaluate")
						{
							bTExpressionNode.ReturnValue = BTExpressionNode.ReturnType.Evaluate;
						}
					}
				}
				else if (node.Name == "priority")
				{
					BTPriorityNode bTPriorityNode = new BTPriorityNode();
					bTNode = bTPriorityNode;
					float result;
					if (node.HasAttribute("refreshrate") && float.TryParse(node.GetAttribute("refreshrate"), out result))
					{
						bTPriorityNode.RefreshRate = result;
					}
					for (int m = 0; m < node.ChildCount; m++)
					{
						ObjectElement child = node.GetChild(m);
						Expression aStartingPriority = null;
						if (child.HasAttribute(ATTR_STARTINGPRIORITY))
						{
							aStartingPriority = ExpressionParser.Parse(child.GetAttribute(ATTR_STARTINGPRIORITY));
						}
						Expression aRunningPriority = null;
						if (child.HasAttribute(ATTR_RUNNINGPRIORITY))
						{
							aRunningPriority = ExpressionParser.Parse(child.GetAttribute(ATTR_RUNNINGPRIORITY));
						}
						bTPriorityNode.SetPriorities(m, aStartingPriority, aRunningPriority);
					}
				}
				else if (node.Name == "selector")
				{
					BTSelectorNode bTSelectorNode = new BTSelectorNode();
					bTNode = bTSelectorNode;
					bool result2;
					if (node.HasAttribute("usepriorities") && bool.TryParse(node.GetAttribute("usepriorities"), out result2))
					{
						bTSelectorNode.UsePriorities = result2;
					}
					for (int n = 0; n < node.ChildCount; n++)
					{
						ObjectElement child2 = node.GetChild(n);
						Expression aStartingPriority2 = null;
						if (child2.HasAttribute(ATTR_PRIORITY))
						{
							aStartingPriority2 = ExpressionParser.Parse(child2.GetAttribute(ATTR_PRIORITY));
						}
						bTSelectorNode.SetPriorities(n, aStartingPriority2, null);
					}
				}
				else if (node.Name == "sequencer")
				{
					BTSequencerNode bTSequencerNode = new BTSequencerNode();
					bTNode = bTSequencerNode;
					bool result3;
					if (node.HasAttribute("usepriorities") && bool.TryParse(node.GetAttribute("usepriorities"), out result3))
					{
						bTSequencerNode.UsePriorities = result3;
					}
					for (int num = 0; num < node.ChildCount; num++)
					{
						ObjectElement child3 = node.GetChild(num);
						Expression aStartingPriority3 = null;
						if (child3.HasAttribute(ATTR_PRIORITY))
						{
							aStartingPriority3 = ExpressionParser.Parse(child3.GetAttribute(ATTR_PRIORITY));
						}
						bTSequencerNode.SetPriorities(num, aStartingPriority3, null);
					}
				}
				else if (node.Name == "constraint")
				{
					BTConstraintNode bTConstraintNode = new BTConstraintNode();
					bTNode = bTConstraintNode;
					if (node.HasAttribute("constraint"))
					{
						bTConstraintNode.Constraint = ExpressionParser.Parse(node.GetAttribute("constraint"));
					}
				}
				else if (node.Name == "random")
				{
					BTRandomNode bTRandomNode = new BTRandomNode();
					bTNode = bTRandomNode;
					for (int num2 = 0; num2 < node.ChildCount; num2++)
					{
						ObjectElement child4 = node.GetChild(num2);
						if (child4.HasAttribute(ATTR_WEIGHT))
						{
							bTRandomNode.SetWeight(num2, ExpressionParser.Parse(child4.GetAttribute(ATTR_WEIGHT)));
						}
					}
				}
				else if (node.Name == "yield")
				{
					bTNode = new BTYieldNode();
				}
				else if (node.Name == "timer")
				{
					BTTimerNode bTTimerNode = new BTTimerNode();
					bTNode = bTTimerNode;
					if (node.HasAttribute("waitforsec"))
					{
						bTTimerNode.WaitTime = ExpressionParser.Parse(node.GetAttribute("waitforsec"));
					}
					if (node.HasAttribute("returnvalue"))
					{
						if (node.GetAttribute("returnvalue") == "success")
						{
							bTTimerNode.ReturnValue = BTTimerNode.ReturnType.Success;
						}
						else if (node.GetAttribute("returnvalue") == "failure")
						{
							bTTimerNode.ReturnValue = BTTimerNode.ReturnType.Failure;
						}
					}
				}
				else if (node.Name == "iterator")
				{
					BTIteratorNode bTIteratorNode = new BTIteratorNode();
					bTNode = bTIteratorNode;
					if (node.HasAttribute("count"))
					{
						bTIteratorNode.CountInitializer = ExpressionParser.Parse(node.GetAttribute("count"));
					}
				}
				else if (node.Name == "parallel")
				{
					BTParallelNode bTParallelNode = new BTParallelNode();
					bTNode = bTParallelNode;
					if (node.HasAttribute("fail"))
					{
						if (node.GetAttribute("fail") == "any")
						{
							bTParallelNode.failOnSingle = true;
						}
						else if (node.GetAttribute("fail") == "all")
						{
							bTParallelNode.failOnSingle = false;
						}
					}
					if (node.HasAttribute("succeed"))
					{
						if (node.GetAttribute("succeed") == "any")
						{
							bTParallelNode.succeedOnSingle = true;
						}
						else if (node.GetAttribute("succeed") == "all")
						{
							bTParallelNode.succeedOnSingle = false;
						}
					}
					if (node.HasAttribute("tiebreaker"))
					{
						if (node.GetAttribute("tiebreaker") == "fail")
						{
							bTParallelNode.failOnTie = true;
						}
						else if (node.GetAttribute("tiebreaker") == "succeed")
						{
							bTParallelNode.failOnTie = false;
						}
					}
				}
				else if (node.Name == "treebinding")
				{
					BTDynamicBindingNode bTDynamicBindingNode = new BTDynamicBindingNode();
					bTNode = bTDynamicBindingNode;
					if (node.HasAttribute("binding"))
					{
						bTDynamicBindingNode.bindingName = node.GetAttribute("binding");
					}
					if (bindings != null)
					{
						bTDynamicBindingNode.LoadTree(bindings);
					}
				}
				else if (node.Name == "move")
				{
					BTActionNode bTActionNode2 = new BTActionNode();
					bTNode = bTActionNode2;
					MoveAction moveAction = (MoveAction)(bTActionNode2.actionInstance = new MoveAction());
					if (node.HasAttribute("movetarget"))
					{
						moveAction.moveTargetExpression = ExpressionParser.Parse(node.GetAttribute("movetarget"));
					}
					if (node.HasAttribute("facetarget"))
					{
						moveAction.faceTargetExpression = ExpressionParser.Parse(node.GetAttribute("facetarget"));
					}
					if (node.HasAttribute("movespeed"))
					{
						moveAction.moveSpeed = ExpressionParser.Parse(node.GetAttribute("movespeed"));
					}
					if (node.HasAttribute("turnspeed"))
					{
						moveAction.turnSpeed = ExpressionParser.Parse(node.GetAttribute("turnspeed"));
					}
					if (node.HasAttribute("closeenoughdistance"))
					{
						moveAction.closeEnoughDistance = ExpressionParser.Parse(node.GetAttribute("closeenoughdistance"));
					}
					if (node.HasAttribute("closeenoughangle"))
					{
						moveAction.closeEnoughAngle = ExpressionParser.Parse(node.GetAttribute("closeenoughangle"));
					}
				}
				else if (node.Name == "animate")
				{
					BTActionNode bTActionNode3 = new BTActionNode();
					bTNode = bTActionNode3;
					AnimateAction animateAction = (AnimateAction)(bTActionNode3.actionInstance = new AnimateAction());
					if (node.HasAttribute("animationstate"))
					{
						animateAction.animationState = node.GetAttribute("animationstate");
					}
				}
				else if (node.Name == "audio")
				{
					BTActionNode bTActionNode4 = new BTActionNode();
					bTNode = bTActionNode4;
					AudioAction audioAction = (AudioAction)(bTActionNode4.actionInstance = new AudioAction());
					if (node.HasAttribute("audiosource"))
					{
						audioAction.audioSourceName = node.GetAttribute("audiosource");
					}
					float result4;
					if (node.HasAttribute("delay") && float.TryParse(node.GetAttribute("delay"), out result4))
					{
						audioAction.delay = result4;
					}
					bool result5;
					if (node.HasAttribute("waituntildone") && bool.TryParse(node.GetAttribute("waituntildone"), out result5))
					{
						audioAction.waitUntilDone = result5;
					}
					bool result6;
					if (node.HasAttribute("forcestoponexit") && bool.TryParse(node.GetAttribute("forcestoponexit"), out result6))
					{
						audioAction.forceAudioStopOnExit = result6;
					}
				}
				else if (node.Name == "detect")
				{
					BTActionNode bTActionNode5 = new BTActionNode();
					bTNode = bTActionNode5;
					DetectAction detectAction = (DetectAction)(bTActionNode5.actionInstance = new DetectAction());
					if (node.HasAttribute("aspect"))
					{
						detectAction.aspect = ExpressionParser.Parse(node.GetAttribute("aspect"));
					}
					if (node.HasAttribute("sensor"))
					{
						detectAction.sensor = ExpressionParser.Parse(node.GetAttribute("sensor"));
					}
					if (node.HasAttribute("entityobjectvariable"))
					{
						detectAction.entityObjectVariable = node.GetAttribute("entityobjectvariable");
					}
					if (node.HasAttribute("aspectvariable"))
					{
						detectAction.aspectVariable = node.GetAttribute("aspectvariable");
					}
					if (node.HasAttribute("aspectobjectvariable"))
					{
						detectAction.aspectObjectVariable = node.GetAttribute("aspectobjectvariable");
					}
				}
				else if (node.Name == "waypoint" || node.Name == "waypointpatrol" || node.Name == "waypointpath" || node.Name == "waypointcustom")
				{
					BTWaypointNode bTWaypointNode = new BTWaypointNode();
					bTNode = bTWaypointNode;
					if (node.HasAttribute("waypointactiontype"))
					{
						switch (node.GetAttribute("waypointactiontype"))
						{
						case "patrol":
							bTWaypointNode.waypointActionType = BTWaypointNode.WaypointActionType.PATROL;
							break;
						case "path":
							bTWaypointNode.waypointActionType = BTWaypointNode.WaypointActionType.PATH;
							break;
						case "custom":
							bTWaypointNode.waypointActionType = BTWaypointNode.WaypointActionType.CUSTOM;
							break;
						}
					}
					if (node.HasAttribute("traversetype"))
					{
						switch (node.GetAttribute("traversetype"))
						{
						case "pingpong":
							bTWaypointNode.traverseType = BTWaypointNode.WaypointTraverseType.PINGPONG;
							break;
						case "loop":
							bTWaypointNode.traverseType = BTWaypointNode.WaypointTraverseType.LOOP;
							break;
						case "oneway":
							bTWaypointNode.traverseType = BTWaypointNode.WaypointTraverseType.ONEWAY;
							break;
						}
					}
					if (node.HasAttribute("traverseorder"))
					{
						string attribute = node.GetAttribute("traverseorder");
						if (attribute == "forward")
						{
							bTWaypointNode.traverseOrder = BTWaypointNode.WaypointTraverseOrder.FORWARD;
						}
						else if (attribute == "reverse")
						{
							bTWaypointNode.traverseOrder = BTWaypointNode.WaypointTraverseOrder.REVERSE;
						}
					}
					if (node.HasAttribute("waypointsetvariable"))
					{
						bTWaypointNode.waypointSetVariable = ExpressionParser.Parse(node.GetAttribute("waypointsetvariable"));
					}
					if (node.HasAttribute("pathtargetvariable"))
					{
						bTWaypointNode.pathTargetExpression = ExpressionParser.Parse(node.GetAttribute("pathtargetvariable"));
					}
					if (node.HasAttribute("movetargetvariable"))
					{
						bTWaypointNode.moveTargetVariable = node.GetAttribute("movetargetvariable");
					}
				}
				else if (node.Name == "mecparam")
				{
					BTActionNode bTActionNode6 = new BTActionNode();
					bTNode = bTActionNode6;
					MecanimParameterAction mecanimParameterAction = (MecanimParameterAction)(bTActionNode6.actionInstance = new MecanimParameterAction());
					if (node.HasAttribute("parametername"))
					{
						mecanimParameterAction.ParameterName = node.GetAttribute("parametername");
					}
					if (node.HasAttribute("valueexpression"))
					{
						mecanimParameterAction.ValueExpression = ExpressionParser.Parse(node.GetAttribute("valueexpression"));
					}
					if (node.HasAttribute("damptime"))
					{
						try
						{
							mecanimParameterAction.DampTime = float.Parse(node.GetAttribute("damptime"));
						}
						catch
						{
						}
					}
					if (node.HasAttribute("parametertype"))
					{
						switch (node.GetAttribute("parametertype"))
						{
						case "boolean":
							mecanimParameterAction.ParameterType = MecanimParameterValue.SupportedMecanimType.Boolean;
							break;
						case "integer":
							mecanimParameterAction.ParameterType = MecanimParameterValue.SupportedMecanimType.Integer;
							break;
						case "float":
							mecanimParameterAction.ParameterType = MecanimParameterValue.SupportedMecanimType.Float;
							break;
						case "trigger":
							mecanimParameterAction.ParameterType = MecanimParameterValue.SupportedMecanimType.Trigger;
							break;
						}
					}
				}
				else if (node.Name == "mecik")
				{
					BTActionNode bTActionNode7 = new BTActionNode();
					bTNode = bTActionNode7;
					MecanimIKAction mecanimIKAction = (MecanimIKAction)(bTActionNode7.actionInstance = new MecanimIKAction());
					if (node.HasAttribute("ikpositiontarget"))
					{
						mecanimIKAction.ikPositionTargetExpression = ExpressionParser.Parse(node.GetAttribute("ikpositiontarget"));
					}
					if (node.HasAttribute("ikrotationtarget"))
					{
						mecanimIKAction.ikRotationTargetExpression = ExpressionParser.Parse(node.GetAttribute("ikrotationtarget"));
					}
					if (node.HasAttribute("positionweight"))
					{
						mecanimIKAction.ikPositionWeightExpression = ExpressionParser.Parse(node.GetAttribute("positionweight"));
					}
					if (node.HasAttribute("rotationweight"))
					{
						mecanimIKAction.ikRotationWeightExpression = ExpressionParser.Parse(node.GetAttribute("rotationweight"));
					}
					if (node.HasAttribute("lookatweight"))
					{
						mecanimIKAction.ikLookAtWeightExpression = ExpressionParser.Parse(node.GetAttribute("lookatweight"));
					}
					if (node.HasAttribute("lookatbodyweight"))
					{
						mecanimIKAction.ikLookAtBodyWeightExpression = ExpressionParser.Parse(node.GetAttribute("lookatbodyweight"));
					}
					if (node.HasAttribute("lookatheadweight"))
					{
						mecanimIKAction.ikLookAtHeadWeightExpression = ExpressionParser.Parse(node.GetAttribute("lookatheadweight"));
					}
					if (node.HasAttribute("lookateyesweight"))
					{
						mecanimIKAction.ikLookAtEyesWeightExpression = ExpressionParser.Parse(node.GetAttribute("lookateyesweight"));
					}
					if (node.HasAttribute("lookatclampweight"))
					{
						mecanimIKAction.ikLookAtClampWeightExpression = ExpressionParser.Parse(node.GetAttribute("lookatclampweight"));
					}
					if (node.HasAttribute("maxturnrate"))
					{
						mecanimIKAction.ikMaxTurnRateExpression = ExpressionParser.Parse(node.GetAttribute("maxturnrate"));
					}
					if (node.HasAttribute("iktype"))
					{
						switch (node.GetAttribute("iktype"))
						{
						case "leftfoot":
							mecanimIKAction.ikType = MecanimAnimator.SupportedMecanimIKType.LeftFoot;
							break;
						case "rightfoot":
							mecanimIKAction.ikType = MecanimAnimator.SupportedMecanimIKType.RightFoot;
							break;
						case "lefthand":
							mecanimIKAction.ikType = MecanimAnimator.SupportedMecanimIKType.LeftHand;
							break;
						case "righthand":
							mecanimIKAction.ikType = MecanimAnimator.SupportedMecanimIKType.RightHand;
							break;
						case "lookat":
							mecanimIKAction.ikType = MecanimAnimator.SupportedMecanimIKType.LookAt;
							break;
						}
					}
				}
				else if (node.Name == "mecstate")
				{
					BTActionNode bTActionNode8 = new BTActionNode();
					bTNode = bTActionNode8;
					MecanimStateAction mecanimStateAction = (MecanimStateAction)(bTActionNode8.actionInstance = new MecanimStateAction());
					if (node.HasAttribute("animationstate"))
					{
						mecanimStateAction.stateExpression = ExpressionParser.Parse(node.GetAttribute("animationstate"));
					}
					if (node.HasAttribute("animationlayer"))
					{
						mecanimStateAction.layerExpression = ExpressionParser.Parse(node.GetAttribute("animationlayer"));
					}
				}
				if (bTNode != null)
				{
					if (node.HasAttribute("name"))
					{
						bTNode.ActionName = node.GetAttribute("name");
					}
					bool result7;
					if (node.HasAttribute(ATTR_DEBUGBREAK) && bool.TryParse(node.GetAttribute(ATTR_DEBUGBREAK), out result7))
					{
						bTNode.DebugBreak = result7;
					}
					if (node.HasAttribute(ATTR_REPEATUNTIL))
					{
						if (node.GetAttribute(ATTR_REPEATUNTIL) == "")
						{
							bTNode.RepeatUntilState = RAINAction.ActionResult.NONE;
						}
						else if (node.GetAttribute(ATTR_REPEATUNTIL) == "success")
						{
							bTNode.RepeatUntilState = RAINAction.ActionResult.SUCCESS;
						}
						else if (node.GetAttribute(ATTR_REPEATUNTIL) == "failure")
						{
							bTNode.RepeatUntilState = RAINAction.ActionResult.FAILURE;
						}
						else if (node.GetAttribute(ATTR_REPEATUNTIL) == "running")
						{
							bTNode.RepeatUntilState = RAINAction.ActionResult.RUNNING;
						}
					}
					for (int num3 = 0; num3 < node.ChildCount; num3++)
					{
						ObjectElement child5 = node.GetChild(num3);
						bTNode.AddChild(LoadNode(child5, bindings, customActionAssembly));
					}
					return bTNode;
				}
				return bTNode;
			}
			catch (Exception ex3)
			{
				Debug.LogWarning("BTLoader: Failed to load Behavior Tree:\r\n" + ex3.Message);
				return null;
			}
		}

		private static List<BTAssetBinding> TrimBindingList(List<BTAssetBinding> bindings, BTAsset removeAsset)
		{
			if (bindings == null)
			{
				return null;
			}
			List<BTAssetBinding> list = new List<BTAssetBinding>();
			foreach (BTAssetBinding binding in bindings)
			{
				if (binding.behaviorTree != removeAsset)
				{
					list.Add(binding);
				}
			}
			return list;
		}

		private static BTAsset GetBoundAsset(List<BTAssetBinding> bindings, string bindingName)
		{
			if (bindings == null)
			{
				return null;
			}
			foreach (BTAssetBinding binding in bindings)
			{
				if (binding.binding == bindingName)
				{
					return binding.behaviorTree;
				}
			}
			return null;
		}

		private static bool ParseVector(string value, out Vector3 vector)
		{
			vector = Vector3.zero;
			try
			{
				if (value == null)
				{
					return false;
				}
				string text = value.Trim();
				if (text.Length < 7)
				{
					return false;
				}
				if (!text.StartsWith("(") || !text.EndsWith(")"))
				{
					return false;
				}
				text = text.Substring(1, text.Length - 2);
				int num = text.IndexOf(',');
				if (num == 0)
				{
					return false;
				}
				vector.x = float.Parse(text.Substring(0, num));
				text = text.Substring(num + 1);
				num = text.IndexOf(',');
				if (num == 0)
				{
					return false;
				}
				vector.y = float.Parse(text.Substring(0, num));
				text = text.Substring(num + 1);
				if (text.Length == 0)
				{
					return false;
				}
				vector.z = float.Parse(text);
				return true;
			}
			catch
			{
			}
			return false;
		}
	}
}
