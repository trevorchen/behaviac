/////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Tencent is pleased to support the open source community by making behaviac available.
//
// Copyright (C) 2015 THL A29 Limited, a Tencent company. All rights reserved.
//
// Licensed under the BSD 3-Clause License (the "License"); you may not use this file except in compliance with
// the License. You may obtain a copy of the License at http://opensource.org/licenses/BSD-3-Clause
//
// Unless required by applicable law or agreed to in writing, software distributed under the License is
// distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and limitations under the License.
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////

#ifndef BEHAVIAC_ENGINESERVICES_UITYPEINTERFACE_H
#define BEHAVIAC_ENGINESERVICES_UITYPEINTERFACE_H

struct UiGenericType
{
public:
    BEHAVIAC_DECLARE_MEMORY_OPERATORS(UiGenericType);

public:
    virtual void SetMemberName(const char* memberName)
    {
        BEHAVIAC_UNUSED_VAR(memberName);
    }
    virtual void SaveDescription(XmlNodeRef& xmlNode) = 0;
    virtual ~UiGenericType()
    {
    }
};

#define UiDescriptorAllocate_DefaultUiInfo  ((UiGenericType*)NULL)

#define DefaultUiInfo DefaultUiInfo

#endif // #ifndef BEHAVIAC_ENGINESERVICES_UITYPEINTERFACE_H
