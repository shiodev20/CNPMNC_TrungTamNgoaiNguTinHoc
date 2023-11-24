<template>
  <div class="list_exam">
    <h1>Danh sách chi nhánh</h1>
    <div class="Upperbar">
      <div class="Upperbar_add">
        <button
          @click="
            this.$router.push({
              name: 'branch',
              query: { action: 'create' },
            })
          "
        >
          Tạo chi nhánh
        </button>
      </div>
    </div>
    <div class="list_exam_child" v-if="list_branch">
      <!-- filter  -->
      <div class="parent_filter">
        <div class="filer_list_exam">
          <input
            v-model="key_search_ap"
            placeholder="Nhập tên chi nhánh"
            class="ip"
            type="text"
          />
          <div class="btn_filter_parent">
            <button class="btn_filter" @click="key_search = key_search_ap">
              Tìm kiếm
            </button>
          </div>
        </div>
        <div>
          <strong>Số lượng {{ list_branch.length }}</strong>
        </div>
      </div>

      <!-- end filter -->
      <!-- List exam -->
      <div class="list_exams">
        <table v-if="list_branch.length > 0" class="table">
          <tr>
            <th>Id</th>
            <th>Tên chi nhánh</th>
            <th>Địa chỉ</th>
            <th>Chức năng</th>
          </tr>
          <tr :id="'item' + item.id" v-for="item in list_branch" :key="item.id">
            <td>{{ item.id }}</td>
            <td>{{ item.name }}</td>
            <td>{{ item.adress }}</td>
            <td class="setting">
              <button
                @click="
                  this.$router.push({
                    name: 'branch',
                    query: { action: 'edit', id: item.id },
                  })
                "
              >
                <font-awesome-icon
                  class="icon_edit"
                  :icon="['fas', 'pen-fancy']"
                />
              </button>
              <button @click="deletes(item.id)">
                <font-awesome-icon
                  class="icon_trash"
                  :icon="['fas', 'trash-can']"
                />
              </button>
            </td>
          </tr>
        </table>
        <div class="images" v-else>
          <img src="../Imagess/notfound.png" alt="" />
        </div>
      </div>
      <!-- end list exam -->
      <!-- pagnigation -->
      <div class="group_page">
        <div
          @click="chage(this_page - 1)"
          v-if="this_page > 1 && count_page > 0"
          class="pre"
        >
          <font-awesome-icon :icon="['fas', 'arrow-left']" />
        </div>
        <div
          :class="this_page == item ? 'show_item_page' : null"
          @click="chage(item)"
          v-for="item in count_page"
          :key="item"
          class="item_page"
        >
          {{ item }}
        </div>
        <div
          @click="chage(this_page + 1)"
          v-if="this_page < count_page"
          class="next"
        >
          <font-awesome-icon :icon="['fas', 'arrow-right']" />
        </div>
      </div>
      <!-- end pagnigation -->
    </div>
  </div>
</template>
<script>
import { computed, ref, watch } from "vue";
import store from "@/store";
import { useToast } from "vue-toastification";
export default {
  name: "HomeView",
  updated() {
    let page_start = this.limit_page * (this.this_page - 1);
    let page_end = this.limit_page * this.this_page - 1;

    for (var item in this.list_branch) {
      const div = document.getElementById("item" + this.list_branch[item].id);
      if (item >= page_start && item <= page_end) {
        div.classList.contains("hide") ? div.classList.remove("hide") : null;
        div.classList.add("show");
      } else {
        div.classList.contains("show") ? div.classList.remove("show") : null;
        div.classList.add("hide");
      }
    }
  },
  setup() {
    store.dispatch("branch/get_all");
    const list_branch = computed(() => {
      if (key_search.value != null && key_search.value != "") {
        return store.state.branch.branch.filter(
          (e) =>
            e.name.toUpperCase().includes(key_search.value.toUpperCase()) ==
            true
        );
      }
      return store.state.branch.branch;
    });
    const deletes = (id) => {
      toast.error("Xóa Thành công");
      store.dispatch("branch/delete", id);
    };

    const this_page = ref(1);
    const limit_page = ref(6);
    const run_app = watch(this_page, () => {
      let page_start = limit_page.value * (this_page.value - 1);
      let page_end = limit_page.value * this_page.value - 1;
      for (var item in list_branch.value) {
        const div = document.getElementById(
          "item" + list_branch.value[item].id
        );
        if (item >= page_start && item <= page_end) {
          div.classList.contains("hide") ? div.classList.remove("hide") : null;
          div.classList.add("show");
        } else {
          div.classList.contains("show") ? div.classList.remove("show") : null;
          div.classList.add("hide");
        }
      }
    });

    const count_page = computed(() => {
      if (list_branch.value == null) {
        return 0;
      }
      return Math.ceil(list_branch.value.length / limit_page.value);
    });

    const chage = (item) => {
      this_page.value = item;
    };

    const key_search = ref(null);
    const key_search_ap = ref(null);
    const toast = useToast();
    return {
      list_branch,
      deletes,
      count_page,
      chage,
      this_page,
      limit_page,
      run_app,
      key_search,
      key_search_ap,
      toast,
    };
  },
};
</script>
<style scoped>
.parent_filter {
  display: flex;
  justify-content: space-between;
  align-items: center;
}
.show_item_page {
  background: var(--color_table);
  color: white !important;
}
.hide {
  display: none;
}
.show {
  display: table-row !important;
}
.ip_name_exambatch {
  padding-left: 10px;
}
/* pagenigation */
.group_page {
  position: absolute;
  display: flex;
  bottom: 15px;
  right: 30px;
}
.group_page > div {
  color: var(--color_table);
  width: 30px;
  height: 30px;
  border-radius: 50%;
  display: flex;
  justify-content: center;
  align-items: center;
  color: var(--color_table);
}

.item_page:hover {
  background: var(--color_table);
  color: white;
}
.pre,
.next {
  border: 1px solid var(--color_table);
}

/* end pagnagation */
/* list exam  */
table {
  border-collapse: collapse;
  width: 100%;
}
.tale {
  margin-left: auto;
  margin-right: auto;
}

th,
td {
  padding: 8px;
  text-align: center;
  vertical-align: middle !important;
}

tr:nth-child(even) {
  background-color: var(--color_row);
}

th {
  background-color: var(--color_table);
  color: white;
}

.setting > button {
  border-radius: 20px;
  background: white;
  border: none;
  margin-left: 10px;
  box-shadow: 0px 4px 4px 0px rgba(0, 0, 0, 0.25);
}

.icon_trash,
.icon_edit,
.icon_detail {
  font-size: 20px;
  padding: 5px;
}
.icon_trash {
  color: red;
}
.icon_edit {
  color: var(--color_edit);
}

/* end list exam  */
/* filter list */
.btn_filter_parent {
  display: flex;
  align-items: center;
  justify-content: center;
}
.filer_list_exam {
  display: flex;
  gap: 10px;
  padding: 10px;
  padding-left: 0;
}
.filer_list_exam > input {
  border-radius: 5px;
  padding-left: 10px;
}
.btn_filter {
  border-radius: 5px;
  cursor: pointer;
  padding: 10px;
  background: var(--color_main);
  color: white;
}
/* end filter list */
/* drop down  */
.dropdown_input > .icon {
  position: absolute;
  right: 10px;
  top: 50%;
  transform: translateY(-50%);
}
.dropdown_input > .text,
.dropdown_input_from > .text {
  position: absolute;
  top: 50%;
  left: 50%;
  color: #071952;
  text-align: center;
  font-family: Roboto;
  font-size: 15px !important;
  font-style: normal;
  font-weight: 500;
  line-height: normal;
  transform: translate(-50%, -50%);
  white-space: nowrap;
}
.dropdown_menu_show {
  opacity: 1 !important;
  pointer-events: visible !important;
}
.dropdown {
  width: 180px;
  margin: 10px;
  position: relative;
}
.dropdown_input {
  position: relative;
  padding: 20px 20px;
  background: white;
  border-radius: 10px;
  border: 1px solid var(--color_main);
  color: var(--color_main);
  font-weight: 600;
}
.dropdown_menu {
  transition: 0.1s;
  margin-top: 3px;
  border: 1px solid var(--color_main);
  position: absolute;
  left: 0;
  right: 0;
  opacity: 0;
  z-index: 1;
  pointer-events: none;
}
.dropdown_item {
  padding: 10px;
  background: white;
}
.dropdown_item:hover {
  background: var(--color_main);
  color: white;
}
/* end drop down  */

h1 {
  color: var(--color_main);
  text-align: center;
  font-family: Roboto;
  font-size: 24px;
  font-style: normal;
  font-weight: 600;
  line-height: normal;
}

/* upperbar */
.Upperbar {
  display: flex;
  width: 912px;
  height: 72px;
  padding: 5px 15px;
  justify-content: space-between;
  align-items: center;
  flex-shrink: 0;
  background: white;
  border-radius: 10px;
}
.Upperbar_add > button {
  padding: 10px 20px;
  color: white;
  border-radius: 10px;
  background: var(--color_main);
}
/* end upperbar */
/*
upper parent right  */
.btn_search > button {
  height: 33px !important;
  border-radius: 10px;
  background: var(--color_main);
  font-size: 12px !important;
  color: white;
}
.upper_parent {
  display: flex;
  gap: 10px;
}
.upper_parent > div {
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: flex-start;
  gap: 5px;
}
.btn_search {
  display: flex;
  justify-content: end !important;
  border-radius: 10px;
}
.upper_parent > div > label {
  color: var(--color_main);
  text-align: center;
  font-family: Roboto;
  font-size: 12px;
  font-style: normal;
  font-weight: 500;
  line-height: normal;
}
.field {
  width: 146px !important;
  height: 33px !important;
  border-radius: 10px;
  border: 1px solid var(--color_main);
  flex-shrink: 0;
  position: relative;
}
.field > .icon {
  position: absolute;
  right: 10px;
  top: 50%;
  transform: translateY(-50%);
}
.dropdown_from {
  position: relative;
}
/* end uppper parent right */

/* list parent  */
.list_exam_child {
  background: white;
  border-radius: 10px;
  padding: 0 20px;
  margin-top: 19px;
  padding-bottom: 67px;
  position: relative;
  height: var(--height_from_admin);
}
/* end list parent  */
</style>
