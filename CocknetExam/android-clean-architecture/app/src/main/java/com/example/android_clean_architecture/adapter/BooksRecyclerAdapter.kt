package com.example.android_clean_architecture.adapter

import android.content.Context
import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.Button
import android.widget.TextView
import androidx.lifecycle.LifecycleOwner
import androidx.recyclerview.widget.RecyclerView
import com.example.android_clean_architecture.R
import com.example.android_clean_architecture.view_model.MyBookViewModel
import com.google.android.material.textfield.TextInputEditText

class BooksRecyclerAdapter(
    private val viewModel: MyBookViewModel,
    private val lifecycleOwner: LifecycleOwner,
    private val token: String,
    private val context: Context?
) :
    RecyclerView.Adapter<BooksRecyclerAdapter.MyViewHolder>() {

    private var itemCount = 1

    init {
        viewModel.booksDataMutable.observe(lifecycleOwner) {
            if (it != null) itemCount = it.size
        }
    }

    class MyViewHolder(itemView: View) : RecyclerView.ViewHolder(itemView) {
        val titleTextView: TextView = itemView.findViewById(R.id.book_title)
        val authorTextView: TextView = itemView.findViewById(R.id.book_author)
//        val addBookButton: Button = itemView.findViewById(R.id.button_add)
//        val deleteBookButton: Button = itemView.findViewById(R.id.button_delete)
//        val bookIdInput: TextInputEditText = itemView.findViewById(R.id.bookIdInput)
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): MyViewHolder =
        MyViewHolder(
            LayoutInflater.from(parent.context).inflate(R.layout.books_list_item, parent, false)
        )

    override fun onBindViewHolder(holder: MyViewHolder, position: Int) {
        viewModel.booksDataMutable.observe(lifecycleOwner) {
            if (it == null) holder.titleTextView.text = "Loading"
            else {
                holder.titleTextView.text = it[position].title
                holder.authorTextView.text = it[position].author

//                holder.addBookButton.setOnClickListener {
//                    viewModel.addBook(token)
//                }
//
//                holder.deleteBookButton.setOnClickListener {
//                    viewModel.deleteBook(token, holder.bookIdInput.text.toString())
//                }
            }
        }
    }

    override fun getItemCount() = itemCount
}