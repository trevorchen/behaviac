// ---------------------------------------------------------------------
/*
This file is auto-generated by behaviac designer, so please don't modify it by yourself!


Usage: include this file in a certain cpp accordingly, only include it once and don't include it in other cpps again.
(RELATIVE_PATH is the path where it is generated):
and you also need to include your agent types' headers before it:

      #include "YourAgentTypes.h"

      #include "RELATIVE_PATH/generated_behaviors.h"
*/

// Export file: exported/behaviac_generated/behaviors/generated_behaviors.h
// ---------------------------------------------------------------------

// You should set the include path of the behaviac lib in your project
// for using the following header files :
#pragma once
#include "behaviac/behaviortree/behaviortree.h"
#include "behaviac/agent/agent.h"
#include "behaviac/agent/taskmethod.h"

#include "behaviac/behaviortree/nodes/actions/action.h"
#include "behaviac/behaviortree/nodes/actions/assignment.h"
#include "behaviac/behaviortree/nodes/actions/compute.h"
#include "behaviac/behaviortree/nodes/actions/noop.h"
#include "behaviac/behaviortree/nodes/actions/wait.h"
#include "behaviac/behaviortree/nodes/actions/waitforsignal.h"
#include "behaviac/behaviortree/nodes/actions/waitframes.h"
#include "behaviac/behaviortree/nodes/composites/compositestochastic.h"
#include "behaviac/behaviortree/nodes/composites/ifelse.h"
#include "behaviac/behaviortree/nodes/composites/parallel.h"
#include "behaviac/behaviortree/nodes/composites/query.h"
#include "behaviac/behaviortree/nodes/composites/referencebehavior.h"
#include "behaviac/behaviortree/nodes/composites/selector.h"
#include "behaviac/behaviortree/nodes/composites/selectorloop.h"
#include "behaviac/behaviortree/nodes/composites/selectorprobability.h"
#include "behaviac/behaviortree/nodes/composites/selectorstochastic.h"
#include "behaviac/behaviortree/nodes/composites/sequence.h"
#include "behaviac/behaviortree/nodes/composites/sequencestochastic.h"
#include "behaviac/behaviortree/nodes/composites/withprecondition.h"
#include "behaviac/behaviortree/nodes/conditions/and.h"
#include "behaviac/behaviortree/nodes/conditions/conditionbase.h"
#include "behaviac/behaviortree/nodes/conditions/condition.h"
#include "behaviac/behaviortree/nodes/conditions/false.h"
#include "behaviac/behaviortree/nodes/conditions/or.h"
#include "behaviac/behaviortree/nodes/conditions/true.h"
#include "behaviac/behaviortree/nodes/decorators/decoratoralwaysfailure.h"
#include "behaviac/behaviortree/nodes/decorators/decoratoralwaysrunning.h"
#include "behaviac/behaviortree/nodes/decorators/decoratoralwayssuccess.h"
#include "behaviac/behaviortree/nodes/decorators/decoratorcount.h"
#include "behaviac/behaviortree/nodes/decorators/decoratorcountlimit.h"
#include "behaviac/behaviortree/nodes/decorators/decoratorfailureuntil.h"
#include "behaviac/behaviortree/nodes/decorators/decoratorframes.h"
#include "behaviac/behaviortree/nodes/decorators/decoratoriterator.h"
#include "behaviac/behaviortree/nodes/decorators/decoratorlog.h"
#include "behaviac/behaviortree/nodes/decorators/decoratorloop.h"
#include "behaviac/behaviortree/nodes/decorators/decoratorloopuntil.h"
#include "behaviac/behaviortree/nodes/decorators/decoratornot.h"
#include "behaviac/behaviortree/nodes/decorators/decoratorrepeat.h"
#include "behaviac/behaviortree/nodes/decorators/decoratorsuccessuntil.h"
#include "behaviac/behaviortree/nodes/decorators/decoratortime.h"
#include "behaviac/behaviortree/nodes/decorators/decoratorweight.h"
#include "behaviac/behaviortree/attachments/event.h"
#include "behaviac/behaviortree/attachments/attachaction.h"
#include "behaviac/behaviortree/attachments/precondition.h"
#include "behaviac/behaviortree/attachments/effector.h"
#include "behaviac/htn/task.h"
#include "behaviac/fsm/fsm.h"
#include "behaviac/fsm/state.h"
#include "behaviac/fsm/startcondition.h"
#include "behaviac/fsm/transitioncondition.h"
#include "behaviac/fsm/waitstate.h"
#include "behaviac/fsm/waitframesstate.h"
#include "behaviac/fsm/alwaystransition.h"
#include "behaviac/fsm/waittransition.h"

// You should set the agent header files of your game
// when exporting cpp files in the behaviac editor:

using namespace behaviac;

// Agent property and method handlers

namespace behaviac
{
	struct METHOD_TYPE_behaviac_Agent_VectorAdd { };
	template<> BEHAVIAC_FORCEINLINE void Agent::_Execute_Method_<METHOD_TYPE_behaviac_Agent_VectorAdd>(IList& p0, System::Object& p1)
	{
		this->behaviac::Agent::VectorAdd(p0, p1);
	}

	struct METHOD_TYPE_behaviac_Agent_VectorClear { };
	template<> BEHAVIAC_FORCEINLINE void Agent::_Execute_Method_<METHOD_TYPE_behaviac_Agent_VectorClear>(IList& p0)
	{
		this->behaviac::Agent::VectorClear(p0);
	}

	struct METHOD_TYPE_behaviac_Agent_VectorContains { };
	template<> BEHAVIAC_FORCEINLINE bool Agent::_Execute_Method_<METHOD_TYPE_behaviac_Agent_VectorContains>(IList& p0, System::Object& p1)
	{
		return this->behaviac::Agent::VectorContains(p0, p1);
	}

	struct METHOD_TYPE_behaviac_Agent_VectorLength { };
	template<> BEHAVIAC_FORCEINLINE int Agent::_Execute_Method_<METHOD_TYPE_behaviac_Agent_VectorLength>(IList& p0)
	{
		return this->behaviac::Agent::VectorLength(p0);
	}

	struct METHOD_TYPE_behaviac_Agent_VectorRemove { };
	template<> BEHAVIAC_FORCEINLINE void Agent::_Execute_Method_<METHOD_TYPE_behaviac_Agent_VectorRemove>(IList& p0, System::Object& p1)
	{
		this->behaviac::Agent::VectorRemove(p0, p1);
	}

}

struct PROPERTY_TYPE_CBTPlayer_m_iBaseSpeed { };
template<> BEHAVIAC_FORCEINLINE unsigned int& CBTPlayer::_Get_Property_<PROPERTY_TYPE_CBTPlayer_m_iBaseSpeed>()
{
	unsigned char* pc = (unsigned char*)this;
	pc += (int)offsetof(CBTPlayer, CBTPlayer::m_iBaseSpeed);
	return *(reinterpret_cast<unsigned int*>(pc));
}

struct METHOD_TYPE_CBTPlayer_Action1 { };
template<> BEHAVIAC_FORCEINLINE behaviac::EBTStatus CBTPlayer::_Execute_Method_<METHOD_TYPE_CBTPlayer_Action1>()
{
	return this->CBTPlayer::Action1();
}

struct METHOD_TYPE_CBTPlayer_Action3 { };
template<> BEHAVIAC_FORCEINLINE behaviac::EBTStatus CBTPlayer::_Execute_Method_<METHOD_TYPE_CBTPlayer_Action3>()
{
	return this->CBTPlayer::Action3();
}

struct METHOD_TYPE_CBTPlayer_Condition { };
template<> BEHAVIAC_FORCEINLINE bool CBTPlayer::_Execute_Method_<METHOD_TYPE_CBTPlayer_Condition>()
{
	return this->CBTPlayer::Condition();
}

namespace behaviac
{
	// Source file: demo_running

	class DecoratorLoop_bt_demo_running_node0 : public DecoratorLoop
	{
	public:
		BEHAVIAC_DECLARE_DYNAMIC_TYPE(DecoratorLoop_bt_demo_running_node0, DecoratorLoop);
		DecoratorLoop_bt_demo_running_node0()
		{
			m_bDecorateWhenChildEnds = true;
		}
	protected:
		virtual int GetCount(Agent* pAgent) const
		{
			BEHAVIAC_UNUSED_VAR(pAgent);
			return 3;
		}
	};

	class Condition_bt_demo_running_node2 : public Condition
	{
	public:
		BEHAVIAC_DECLARE_DYNAMIC_TYPE(Condition_bt_demo_running_node2, Condition);
		Condition_bt_demo_running_node2()
		{
		}
	protected:
		virtual EBTStatus update_impl(Agent* pAgent, EBTStatus childStatus)
		{
			BEHAVIAC_UNUSED_VAR(pAgent);
			BEHAVIAC_UNUSED_VAR(childStatus);
			bool opl = ((CBTPlayer*)pAgent)->_Execute_Method_<METHOD_TYPE_CBTPlayer_Condition, bool >();
			bool opr = true;
			bool op = Details::Equal(opl, opr);
			return op ? BT_SUCCESS : BT_FAILURE;
		}
	};

	class Action_bt_demo_running_node3 : public Action
	{
	public:
		BEHAVIAC_DECLARE_DYNAMIC_TYPE(Action_bt_demo_running_node3, Action);
		Action_bt_demo_running_node3()
		{
		}
	protected:
		virtual EBTStatus update_impl(Agent* pAgent, EBTStatus childStatus)
		{
			BEHAVIAC_UNUSED_VAR(pAgent);
			BEHAVIAC_UNUSED_VAR(childStatus);
			behaviac::EBTStatus result = ((CBTPlayer*)pAgent)->_Execute_Method_<METHOD_TYPE_CBTPlayer_Action1, behaviac::EBTStatus >();
			return result;
		}
	};

	class Action_bt_demo_running_node4 : public Action
	{
	public:
		BEHAVIAC_DECLARE_DYNAMIC_TYPE(Action_bt_demo_running_node4, Action);
		Action_bt_demo_running_node4()
		{
		}
	protected:
		virtual EBTStatus update_impl(Agent* pAgent, EBTStatus childStatus)
		{
			BEHAVIAC_UNUSED_VAR(pAgent);
			BEHAVIAC_UNUSED_VAR(childStatus);
			behaviac::EBTStatus result = ((CBTPlayer*)pAgent)->_Execute_Method_<METHOD_TYPE_CBTPlayer_Action3, behaviac::EBTStatus >();
			return result;
		}
	};

	class bt_demo_running
	{
	public:
		static bool Create(BehaviorTree* pBT)
		{
			pBT->SetClassNameString("BehaviorTree");
			pBT->SetId((uint32_t)-1);
			pBT->SetName("demo_running");
			pBT->SetIsFSM(false);
#if !defined(BEHAVIAC_RELEASE)
			pBT->SetAgentType("CBTPlayer");
#endif
			// children
			{
				DecoratorLoop_bt_demo_running_node0* node0 = BEHAVIAC_NEW DecoratorLoop_bt_demo_running_node0;
				node0->SetClassNameString("DecoratorLoop");
				node0->SetId(0);
#if !defined(BEHAVIAC_RELEASE)
				node0->SetAgentType("CBTPlayer");
#endif
				pBT->AddChild(node0);
				{
					Sequence* node1 = BEHAVIAC_NEW Sequence;
					node1->SetClassNameString("Sequence");
					node1->SetId(1);
#if !defined(BEHAVIAC_RELEASE)
					node1->SetAgentType("CBTPlayer");
#endif
					node0->AddChild(node1);
					{
						Condition_bt_demo_running_node2* node2 = BEHAVIAC_NEW Condition_bt_demo_running_node2;
						node2->SetClassNameString("Condition");
						node2->SetId(2);
#if !defined(BEHAVIAC_RELEASE)
						node2->SetAgentType("CBTPlayer");
#endif
						node1->AddChild(node2);
						node1->SetHasEvents(node1->HasEvents() | node2->HasEvents());
					}
					{
						Action_bt_demo_running_node3* node3 = BEHAVIAC_NEW Action_bt_demo_running_node3;
						node3->SetClassNameString("Action");
						node3->SetId(3);
#if !defined(BEHAVIAC_RELEASE)
						node3->SetAgentType("CBTPlayer");
#endif
						node1->AddChild(node3);
						node1->SetHasEvents(node1->HasEvents() | node3->HasEvents());
					}
					{
						Action_bt_demo_running_node4* node4 = BEHAVIAC_NEW Action_bt_demo_running_node4;
						node4->SetClassNameString("Action");
						node4->SetId(4);
#if !defined(BEHAVIAC_RELEASE)
						node4->SetAgentType("CBTPlayer");
#endif
						node1->AddChild(node4);
						node1->SetHasEvents(node1->HasEvents() | node4->HasEvents());
					}
					node0->SetHasEvents(node0->HasEvents() | node1->HasEvents());
				}
				pBT->SetHasEvents(pBT->HasEvents() | node0->HasEvents());
			}
			return true;
		}
	};

	class CppGenerationManager : GenerationManager
	{
	public:
		CppGenerationManager()
		{
			SetInstance(this);
		}

		virtual void RegisterBehaviorsImplement()
		{
			Workspace::GetInstance()->RegisterBehaviorTreeCreator("demo_running", bt_demo_running::Create);
		}
	};

	CppGenerationManager _cppGenerationManager_;
}
