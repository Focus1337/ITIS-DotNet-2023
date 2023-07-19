package com.example.secretgenerator.adapter

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import androidx.lifecycle.Lifecycle
import androidx.lifecycle.LifecycleObserver
import androidx.lifecycle.LifecycleOwner
import androidx.recyclerview.widget.RecyclerView
import com.example.secretgenerator.R
import com.example.secretgenerator.TodoEntity
import com.example.secretgenerator.viewmodel.HomeViewModel

class TodosRecyclerAdapter(
    private val viewModel: HomeViewModel,
    private val lifecycleOwner: LifecycleOwner
) :
    RecyclerView.Adapter<TodosRecyclerAdapter.ViewHolder>() {

    class ViewHolder(view: View) : RecyclerView.ViewHolder(view) {
        val username: TextView
        val title: TextView
        val completed: TextView

        init {
            username = view.findViewById(R.id.todosCardUsernameTextView)
            title = view.findViewById(R.id.todosCardTitleTextView)
            completed = view.findViewById(R.id.todosCardCompletedTextView)
        }
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): ViewHolder {
        val view =
            LayoutInflater.from(parent.context).inflate(R.layout.todos_row_item, parent, false)
        return ViewHolder(view)
    }

    override fun getItemCount(): Int = 1000//entityArrayList.size

    override fun onBindViewHolder(holder: ViewHolder, position: Int) {
        viewModel.getTodoData(position).observe(lifecycleOwner) {
            if (it == null) {
                holder.title.text = "Loading"
                holder.username.text = "Loading"
                holder.completed.text = "Loading"
            } else {
                holder.title.text = it.title
                holder.username.text = it.id.toString()
                holder.completed.text = it.completed.toString()
            }
        }
    }
}