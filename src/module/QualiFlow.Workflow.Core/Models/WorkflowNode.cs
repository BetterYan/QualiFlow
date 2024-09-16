using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QualiFlow.Workflow.Core.Models;

public class WorkflowNode
{
    public string Name { get; set; }
    public string Description { get; set; }
    public WorkflowNode Parent { get; set; }
    public List<WorkflowNode> ChildrenNodes { get; set; }
    public WorkflowNode()
    {
        ChildrenNodes = new List<WorkflowNode>();
    }
}
